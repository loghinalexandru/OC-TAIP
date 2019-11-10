import numpy
import scipy.signal as signal


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