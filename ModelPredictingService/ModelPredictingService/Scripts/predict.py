import os
import pickle
import argparse
import numpy
import time
import datetime
import generate_model_train

ROOT_DIT = r"to_predict"
USERNAME = r"diana"
MODELS_PATH = r"models"
PREDICTIONS = r"predictions"


def predict(root_dir: str, models_dir: str, username: str, save_to_dir: str):
    results = None
    model_path = os.path.join(models_dir, username + ".h5")
    model = generate_model_train.Model([16, 16, 1], 10, 5, model_path)
    for subdir, _, files in os.walk(root_dir):
        for file in files:
            with open(os.path.join(subdir, file), "rb") as fd:
                data = pickle.load(fd)
                if results is None:
                    results = model.predict(data)
                else:
                    results = numpy.append(results, model.predict(data))
    filename = datetime.datetime.fromtimestamp(time.time()).strftime('%Y-%m-%d %H-%M-%S') + ".txt"
    numpy.savetxt(os.path.join(save_to_dir, filename), results)


if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("--root_to_predict", "-root_dir", default=ROOT_DIT, required=False)
    parser.add_argument("--username", "-usr", default=USERNAME, required=False)
    parser.add_argument("--models_dir", "-models", default=MODELS_PATH, required=False)
    parser.add_argument("--predictions_dir", "-predictions", default=PREDICTIONS, required=False)
    args = parser.parse_args()

    ROOT_DIT = args.root_to_predict
    USERNAME = args.username
    MODELS_PATH = args.models_dir
    PREDICTIONS = args.predictions_dir

    os.mkdir(PREDICTIONS)

    predict(ROOT_DIT, MODELS_PATH, USERNAME, PREDICTIONS)
