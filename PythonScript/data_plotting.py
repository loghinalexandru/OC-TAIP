import os
import numpy
import pandas
import matplotlib.pyplot as plt


def plot_subjects_info(path: str, save_to_dir: str, show_plot=False):
    dataframe = pandas.read_csv(path)
    # print(dataframe)
    for column in dataframe.columns:
        if "code" in column:
            continue
        title = "Subject's " + column
        if "gender" in column:
            plt.hist(dataframe[column].replace([1, 0], ["Male", "Female"]))
        else:
            plt.hist(dataframe[column])
        plt.title(title)
        plt.ylabel("Number of appearances")
        plt.xlabel(column.title())
        plt.savefig(os.path.join(save_to_dir, title+".png"))
        if show_plot:
            plt.show()
    return


def plot_walking():
    return


def process_data_walking(path: str, window_interval= 100):
    dataframe = pandas.read_csv(path)
    raw_means = []
    raw_meadians = []
    return


def plot(directory: str, save_to_dir: str):
    for subdir, dirs, files in os.walk(directory):
        for file in files:
            file_path = os.path.join(subdir, file)
            if "data_subjects_info" in file:
                plot_subjects_info(file_path, save_to_dir, False)
            if "wlk" in subdir:
                print(file_path)
                process_data_walking(file_path)

    return


if __name__ == '__main__':
    print("Plotting data")
    plot(r"..\Dataset", r"..\Resources\Plots")
