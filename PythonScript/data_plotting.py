import os
import numpy
import pandas
import scipy.signal as signal
import matplotlib.pyplot as plt


def plot_subjects_info(path: str, save_to_dir: str, show_plot=False):
    dataframe = pandas.read_csv(path)
    colors = ["purple", "yellow", "green", "orange"]
    index = 0
    # print(dataframe)
    for column in dataframe.columns:
        if "code" in column:
            continue
        title = "Subject's " + column
        if "gender" in column:
            plt.hist(dataframe[column].replace([1, 0], ["Male", "Female"]), color=colors[index % 4])
        else:
            plt.hist(dataframe[column], color=colors[index % 4])
        plt.title(title)
        plt.ylabel("Number of appearances")
        plt.xlabel(column.title())
        plt.savefig(os.path.join(save_to_dir, title + ".png"))
        if show_plot:
            plt.show()
        plt.close()
        index += 1
    return


def peaks_count_distance(matrix: list):
    temp = numpy.array(matrix)
    peaks_x, _ = signal.find_peaks(temp[:, 0])
    peaks_y, _ = signal.find_peaks(temp[:, 1])
    peaks_z, _ = signal.find_peaks(temp[:, 2])
    return [len(peaks_x), len(peaks_y), len(peaks_z)], [peaks_x.mean(), peaks_y.mean(), peaks_z.mean()]


def compute_magnitude(matrix: list, window_interval: int):
    result = numpy.power(matrix, 2)
    result = numpy.sum(result, axis=1)
    result = numpy.sqrt(result)
    result = numpy.sum(result) / window_interval
    return result


def process_data_walking(dataframe: pandas.DataFrame, window_interval=100):
    frequency_domain = numpy.fft.fftn(
        [dataframe['userAcceleration.x'], dataframe['userAcceleration.y'], dataframe['userAcceleration.z']]).T
    dataframe_size = dataframe.shape[0]
    raw_means = []
    raw_meadians = []
    raw_magnitudes = []
    raw_average_peaks = []
    raw_distance_peaks = []
    fft_means = []
    fft_medians = []
    fft_magnitudes = []
    for index in range(0, dataframe_size, window_interval):
        raw_slice = dataframe[index:index + window_interval]
        raw_slice = raw_slice[raw_slice.columns[-3:]]
        raw_means.append(raw_slice.mean().values.tolist())
        raw_meadians.append(raw_slice.median().values.tolist())
        raw_magnitudes.append(compute_magnitude(raw_slice.values.tolist(), window_interval))
        raw_average_peaks.append(peaks_count_distance(raw_slice.values.tolist())[0])
        raw_distance_peaks.append(peaks_count_distance(raw_slice.values.tolist())[1])
        fft_slice = frequency_domain[index:index + 100]
        fft_means.append(list(numpy.mean(fft_slice, axis=0)))
        fft_medians.append(list(numpy.median(fft_slice, axis=0)))
        fft_magnitudes.append(compute_magnitude(fft_slice, window_interval))
    raw_means = numpy.array(raw_means)
    raw_meadians = numpy.array(raw_meadians)
    raw_magnitudes = numpy.array(raw_magnitudes)
    raw_average_peaks = numpy.mean(raw_average_peaks, axis=0)
    raw_distance_peaks = numpy.mean(raw_distance_peaks, axis=0)
    fft_means = numpy.array(fft_means)
    fft_medians = numpy.array(fft_medians)
    fft_magnitudes = numpy.array(fft_magnitudes)
    return raw_means, raw_meadians, raw_magnitudes, raw_average_peaks, raw_distance_peaks, fft_means, fft_medians, fft_magnitudes


def plot_acceleration(dataframe: pandas.DataFrame, file, save_to_dir: str, show_plot=True):
    fig, axis = plt.subplots(3, figsize=(20, 10))
    fig.suptitle(os.path.splitext(file)[0] + " acceleration x, y, x")
    axis[0].plot(range(dataframe.shape[0]), dataframe['userAcceleration.x'])
    axis[1].plot(range(dataframe.shape[0]), dataframe['userAcceleration.y'])
    axis[2].plot(range(dataframe.shape[0]), dataframe['userAcceleration.z'])
    plt.savefig(os.path.join(save_to_dir, os.path.splitext(file)[0] + " acceleration x, y, x.png"))
    if show_plot:
        plt.show()
    plt.close()
    return


def plot_all(directory: str, save_to_dir: str, show_plot=False):
    for subdir, dirs, files in os.walk(directory):
        for file in files:
            file_path = os.path.join(subdir, file)
            if "data_subjects_info" in file:
                plot_subjects_info(file_path, save_to_dir, show_plot)
            if "wlk" in subdir:
                print(file_path)
                dataframe = pandas.read_csv(file_path)
                dir = os.path.split(subdir)[-1]
                if not os.path.exists(os.path.join(save_to_dir, dir)):
                    os.mkdir(os.path.join(save_to_dir, dir))
                plot_acceleration(dataframe, file, os.path.join(save_to_dir, dir), show_plot)
                process_data_walking(dataframe, 100)
    return


if __name__ == '__main__':
    print("Plotting data")
    plot_all(r"..\Dataset", r"..\Resources\Plots")
