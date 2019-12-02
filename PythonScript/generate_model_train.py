import keras
import numpy as np
from keras.models import Sequential
from keras.layers import Dense, Activation, Dropout
import os
import pickle
import numpy
import random


SAVE_PATH = r"models"
PROCESSED_DATA_DIR = r"processed_data"


class Model:
    def __init__(self, layer_sizes: list, input_size: int):
        self.model = Sequential()
        self.model.add(Dense(layer_sizes[0], input_shape=(input_size,), activation='relu'))
        self.model.add(Dropout(0.5))
        self.model.add(Dense(layer_sizes[1], activation='relu'))
        self.model.add(Dropout(0.5))
        self.model.add(Dense(layer_sizes[2], activation='softmax'))
        return

    def compile(self):
        self.model.compile(loss='categorical_crossentropy', optimizer='rmsprop', metrics=['accuracy'])
        return

    def train(self, x_train: np.array, y_train: np.array, x_validation: np.array, y_validation: np.array, batch_size=32,
              epochs=5):
        self.model.fit(x_train, y_train, batch_size=batch_size, epochs=epochs, shuffle=True,
                       validation_data=(x_validation, y_validation))
        return

    def save(self, path: str):
        self.model.save(filepath=path)
        return


def split_train_validation(processed_data_dir: str, subjects: list, split_procentage=0.2):
    x_data = None
    y_data = None
    for subdir, _, files in os.walk(processed_data_dir):
        subject = os.path.split(subdir)[-1]
        if subject in subjects:
            for file in files:
                with open(os.path.join(subdir, file), "rb") as fd:
                    saved_data = pickle.load(fd)
                    if x_data is None:
                        x_data = saved_data
                    else:
                        x_data = numpy.append(x_data, saved_data, axis=0)
                    labels = numpy.repeat(int(subject), saved_data.shape[0])
                    if y_data is None:
                        y_data = labels
                    else:
                        y_data = numpy.append(y_data, labels)
    validation_indeces = random.sample(range(x_data.shape[0]), int(split_procentage*x_data.shape[0]))
    train_indeces = list(set(range(x_data.shape[0])) - set(validation_indeces))
    train = zip(x_data[train_indeces],  y_data[train_indeces])
    validation = zip(x_data[validation_indeces], y_data[validation_indeces])
    return train, validation


if __name__ == '__main__':
    # train each model separately
    train, validation = split_train_validation(PROCESSED_DATA_DIR, ['1', '2', '3', '4'])
    x_train, y_train = zip(*train)
    x_validation, y_validation = zip(*validation)
    model = Model([64, 64, 1], 10)
    model.compile()
    model.train(x_train, y_train, x_validation, y_validation)
