import keras
import tensorflow
from keras.models import Sequential
from keras.layers import Dense, Activation


class Model:
    def __init__(self, batch_size: int, size_input: int):
        self.model = Sequential()
        self.model.add(Dense(batch_size, input_shape=(size_input,)))
        self.model.add(Activation('relu'))
        self.model.add()
        return
