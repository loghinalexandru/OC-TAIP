# State Of The Art
***
### What did others?
There are multiple papers that involve this kind of authentication. Some of them state that they used a device strapped to the lower leg of a person to measure the acceleration on all the 3 axis in order to authenticate someone. More recent papers have been using smarthphones to gain the accelerometer data since it is already integrated in the phone, but there are no commercial use application that use this kind of technology in order to add another security layer for the ordinary user.
***
### Names in the field
The first person to introduce the idea of biometric gait authentication was **Ailisto, H.** in his most known paper **Identifying People from Gait Pattern with Accelerometers** done in 2005. Since then other big names have emerged in this field like **Thingom Bishal Singha**, **Rajsekhar Kumar Nath** and **Dr. A. V. Narsimhadhan**.
***
### Results
As stated in the cited papers below the usual approach for this authentication method is to extract data from the embedded accelerometer sensor from a user phone and pass this data to a machine learning algorithm in order to determine if the user is who he claims he is (authorization) or to determine who the user is (authentication).
As an article stated based on the experimental results from building a Random Forest based on both time and frequenc, the resultant model delivers an accuracy of 0.9679 and Area under Curve (AUC) of 0.9822.
***
### Methodology

#### A. Data Preprocessing
1. Data Collection:
2. Feature Extraction:
3. Dataset Creation:

#### B. Model
1. Decision Tree Classifier:
2. Random Forest Ensemble:
***
### Related Articles
There are many articles that describe the steps of data gathering and how the algorithm for the classification worked. They also state that there was a prediction accuracy from 85% and up depending on the method used to classify the data. Below is a list of articles that describe the process of this method in order to authenticate a user:

 - [Biometric Gait Authentication Using Accelerometer Sensor](http://www.jcomputers.us/vol1/jcp0107-06.pdf)
 - [Person Recognition using Smartphones’ Accelerometer Data](https://arxiv.org/pdf/1711.04689.pdf)
 - [A Lightweight Gait Authentication on Mobile Phone Regardless of Installation Error](https://link.springer.com/content/pdf/10.1007%2F978-3-642-39218-4_7.pdf)

#### [Lifelong Learning with Dynamically Expandable Networks](https://github.com/loghinalexandru/OC-TAIP/blob/master/Resources/A%20Novel%20Progressive%20Learning%20Technique%20for%20Multi-class%20Classification.pdf)

In order to satify the need for scalability we can implement a dinamically expandable network to face the increasing number of users.
Big names in this field are **Jeongtae Lee**, **Jaehong Yoon**, **Eunho Yang**, **Sung ju hwang**. The model can dynamically decide its network capacity as it trains on a sequence of tasks, to learn a compact overlapping knowledge sharing structure among tasks.
It is trained in an online manner by performing selective retraining, dynamically expands network capacity upon arrival of each task with only the necessary number of units, and effectively prevents semantic drift by splitting/duplicating units and timestamping them.
Results have shown that it outperforms the existing lifelong learning methods, achieving almost the same performance as the network trained in batch while using as little as 11.9% − 19.1% of its capacity.

#### How it is done:
- Incremental Learning of a Dynamically Expandable Network
- Selective retraining
- Dynamic network expansion
- Network split/duplication
***
### Resources
For now I did not find any tool to help us further in the project requirements but I did manage to find a dataset of acceloremeter readings for a preliminary test in order to validate the posibility of this method. The dataset can be found at the next link:

 - [motionsense-dataset](https://www.kaggle.com/malekzadeh/motionsense-dataset)
 ***
 
### Technology Stack
- .Net Core 3.0
- Keras
- Android SDK
- Microsoft SQL Server Express
- Entity Framework
 ***

### Libraries
- absl-py              0.8.1
- astor                0.8.0
- cycler               0.10.0
- gast                 0.2.2
- google-pasta         0.1.7
- grpcio               1.24.1
- h5py                 2.10.0
- Keras                2.3.1
- Keras-Applications   1.0.8
- Keras-Preprocessing  1.1.0
- kiwisolver           1.1.0
- Markdown             3.1.1
- matplotlib           3.1.1
- numpy                1.17.2
- opt-einsum           3.1.0
- pandas               0.25.1
- pip                  19.0.3
- protobuf             3.10.0
- pyparsing            2.4.2
- python-dateutil      2.8.0
- pytz                 2019.3
- PyYAML               5.1.2
- scipy                1.3.1
- setuptools           40.8.0
- six                  1.12.0
- tensorboard          2.0.0
- tensorflow           2.0.0
- tensorflow-estimator 2.0.0
- termcolor            1.1.0
- Werkzeug             0.16.0
- wheel                0.33.6
- wrapt                1.11.2
