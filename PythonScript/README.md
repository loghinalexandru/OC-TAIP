# Update 4.01.2020
- Improved model training
- Removed NaN values from input feed
- Removed NaN values from processed data

|Subject|Loss|Accuracy|Training size|Loss|Validation accuracy|Validation size|
|---|---|---|--|---|---|---|
|alex6|0.4867|0.7907|3487|nan|0.8434|8220|
|alex11|nan|0.0000|9999|nan|0.0000|8220|
|alex2|0.7079|0.5429|140|0.7033|0.2917|8220|
|teodora|0.2497|0.9349|3427|nan|0.9478|8220|
|alex3|0.3953|0.8431|8746|0.4292|0.7765|8220|
|alex4|0.4863|0.7792|7695|0.4561|0.7118|8220|
|alex8|0.6570|0.6350|2649|0.6753|0.4507|8220|
|alex16|0.6655|0.6161|5001|0.6336|0.3393|8220|
|diana|0.6312|0.6319|182|0.5853|0.8287|8220|
|alex12|nan|0.0000|10038|nan|0.0000|8220|
|alex14|nan|0.0000|7879|nan|0.0000|8220|
|alex|0.7608|0.5714|56|nan|0.7681|8220|
|alex15|0.5414|0.7513|7975|0.5697|0.6386|8220|
|alex10|0.6428|0.6156|2427|0.6110|0.4748|8220|
|alex13|0.5552|0.7505|5792|0.6679|0.6045|8220|
|alex1|0.7100|0.5542|83|0.5928|0.8860|8220|
|alex7|0.5507|0.7515|2765|0.4676|0.8552|8220|

## Notes
Be careful when uploading the csv files. When reading the data from Dataset\CollectedData the followings were found:
- alex12.csv: 1 Null value/column
- alex11.csv: 1 Null value on column z

When collecting data through the phone, the file stops writing immediately, sometimes right in the middle of a new row. Check the end of the above files.

## To do
- [ ] Solve training issues for alex11, alex12 and alex14
- [ ] Further improve training
