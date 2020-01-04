import numpy as np
from keras.models import Sequential
from keras.layers import Dense, Dropout
import os
import pickle
import numpy
import random
import argparse

SAVE_PATH = r"models"
PROCESSED_DATA_DIR = r"processed_data"


class Model:
    def __init__(self, layer_sizes: list, input_size: int, batch_size=5, path_load=None):
        self.model = Sequential()
        self.model.add(Dense(layer_sizes[0], input_shape=(input_size,), activation='relu'))
        self.model.add(Dropout(0.5))
        self.model.add(Dense(layer_sizes[1], activation='relu'))
        self.model.add(Dropout(0.5))
        self.model.add(Dense(layer_sizes[2], activation='sigmoid'))
        self.batch_size = batch_size
        if path_load:
            self.model.load_weights(path_load)
        return

    def compile(self):
        self.model.compile(loss='binary_crossentropy', optimizer='rmsprop', metrics=['accuracy'])
        return

    def train(self, x_train: np.array, y_train: np.array, x_validation: np.array, y_validation: np.array, batch_size=5,
              epochs=20):
        self.model.fit(x_train, y_train, batch_size=batch_size, epochs=epochs, shuffle=True,
                       validation_data=(x_validation, y_validation))
        return

    def predict(self, to_predict: numpy.array):
        y_predicted = self.model.predict(to_predict, batch_size=self.batch_size)
        return y_predicted

    def save(self, path: str):
        self.model.save(filepath=path)
        return


def get_numpy(processed_data_dir: str):
    x_data = None
    y_data = None
    for subdir, _, files in os.walk(processed_data_dir):
        subject = os.path.split(subdir)[-1]
        for file in files:
            with open(os.path.join(subdir, file), "rb") as fd:
                saved_data = pickle.load(fd)
                if x_data is None:
                    x_data = saved_data
                else:
                    x_data = numpy.append(x_data, saved_data, axis=0)
                labels = numpy.repeat(subject, saved_data.shape[0])
                if y_data is None:
                    y_data = labels
                else:
                    y_data = numpy.append(y_data, labels)
    data = zip(x_data, y_data)
    return data


def train_models(data: numpy.array, save_to: str, procentage_validation=0.2):
    input, output = zip(*data)
    input = numpy.array(list(input))
    output = numpy.array(list(output))

    for subject in set(output):
        print("Subject: {}".format(subject))
        indexes = numpy.where(output == subject)[0]
        indexes = numpy.append(indexes, random.sample(range(len(output)), len(indexes)))
        indexes = numpy.unique(indexes)
        x_train = input[indexes]
        y_train = (output[indexes] == subject).astype(numpy.float)
        y_train = y_train.T

        indexes = random.sample(range(input.shape[0]), int(procentage_validation * input.shape[0]))
        x_validation = input[indexes]
        y_validation = (output[indexes] == subject).astype(numpy.float)
        y_validation = y_validation.T

        model = Model([16, 16, 1], 10)
        model.compile()
        model.train(x_train, y_train, x_validation, y_validation)
        model.save(os.path.join(save_to, str(subject)+".h5"))
    return


if __name__ == '__main__':
    # train each model separately
    parser = argparse.ArgumentParser()
    parser.add_argument("--save_models_dir", "-save_path", default=SAVE_PATH, required=False)
    parser.add_argument("--processed_data_dir", "-processed_dir", default=PROCESSED_DATA_DIR, required=False)
    args = parser.parse_args()

    PROCESSED_DATA_DIR = args.processed_data_dir
    SAVE_PATH = args.save_models_dir

    data = get_numpy(PROCESSED_DATA_DIR)
    train_models(data, save_to=SAVE_PATH)
