import unittest
from PythonScript import feature_extraction


class TestFeatureExtraction(unittest.TestCase):
    def test_means(self):
        self.assertEqual(feature_extraction.get_mean(), 0)

    def test_medians(self):
        self.assertEqual(feature_extraction.get_medians(), 0)

    def test_magnitudes(self):
        self.assertEqual(feature_extraction.get_magnitudes([], 100), 0)

    def test_average_peaks(self):
        self.assertEqual(feature_extraction.get_average_peaks(), 0)

    def test_distance_peaks(self):
        self.assertEqual(feature_extraction.get_distance_peaks(), 0)


if __name__ == '__main__':
    if __name__ == '__main__':
        unittest.main()
