import os
import numpy
import pandas as pd
import scipy.signal as signal
import pickle
import datetime
import time

ROOT_DIR = r"raw_data"
SAVE_DIR = r"processed_data"


def get_magnitudes(matrix: list, window_interval: int):
    result = numpy.power(matrix, 2)
    result = numpy.sum(result, axis=1)
    result = numpy.sqrt(result)
    result = numpy.sum(result) / window_interval
    return result


def peaks_count_distance(matrix: list):
    temp = numpy.array(matrix)
    peaks_x, _ = signal.find_peaks(temp[:, 0])
    peaks_y, _ = signal.find_peaks(temp[:, 1])
    peaks_z, _ = signal.find_peaks(temp[:, 2])
    return [len(peaks_x), len(peaks_y), len(peaks_z)], [peaks_x.mean(), peaks_y.mean(), peaks_z.mean()]


def get_instance_input(results: list):
    instance_input = []
    for result in results:
        if type(result) is not list:
            instance_input.append(result)
        else:
            for item in result:
                instance_input.append(item)
    return instance_input


def process_file(filepath: str, window_interval=100):
    dataframe = pd.read_csv(filepath)
    dataframe_size = dataframe.shape[0]
    result = []

    for index in range(0, dataframe_size, window_interval):
        raw_slice = dataframe[index:index + window_interval]
        raw_slice = raw_slice[raw_slice.columns[-3:]]

        means = raw_slice.mean().values.tolist()
        medians = raw_slice.median().values.tolist()
        magnitudes = get_magnitudes(raw_slice.values.tolist(), window_interval)
        peak_distances = peaks_count_distance(raw_slice.values.tolist())[0]

        result.append(get_instance_input([means, medians, magnitudes, peak_distances]))

    result = numpy.array(result)
    return result


def process_all(root_dir: str, save_dir: str, window_frame=100):
    for subdir, _, files in os.walk(root_dir):
        for file in files:
            filepath = os.path.join(subdir, file)
            subject = os.path.splitext(file)[0].split("_")[-1]
            save_to_dir = os.path.join(save_dir, subject)
            if not os.path.exists(save_to_dir):
                os.mkdir(save_to_dir)
            result = process_file(filepath=filepath, window_interval=window_frame)
            print(result)
            save_path = os.path.join(save_to_dir, datetime.datetime.fromtimestamp(time.time()).strftime(
                                         '%Y-%m-%d %H-%M-%S') + ".pickle")
            with open(save_path, "wb") as fd:
                pickle.dump(result, fd)
    return


if __name__ == '__main__':
    print("Processing data.")
    process_all(root_dir=ROOT_DIR, save_dir=SAVE_DIR)
