train = read.csv("activity_train.csv", header=TRUE)
test = read.csv("activity_test.csv", header=TRUE)
train$activity = as.factor(train$activity)
test$activity = as.factor(test$activity)

#1
plot(train$activity, ylab="Frequency", xlab="Class name")

plot(train$tBodyAcc_IQR_2, train$tGravityAcc_Mean_2, col=(train$activity), pch=16)

plot(train$tBodyAcc_Mean_1, train$tBodyAcc_STD_1, col=(train$activity), pch=16)

plot(train$tBodyAcc_Correlation_1, train$tBodyAccJerk_ARCoeff_10, col=(train$activity), pch=16)

plot(train$fBodyAcc_Kurtosis_1, train$fBodyAcc_BandsEnergyOld_10, col=(train$activity), pch=16)

#2
rf=randomForest(activity~.,data=train,ntree=100,importance=T, mtry=50)
#plot(rf)
#varImpPlot(rf)
pred=predict(rf,test)
mean(pred==test$activity)*100
confMat=table(true_lab=test$activity, predict=pred)
#confMat
#diag(confMat)/rowSums(confMat)

#3
train_label=as.numeric(train$activity)-1
test_label=as.numeric(test$activity)-1
bst = xgboost(data = as.matrix(train[,1:561]), label=train_label, num_class=12, max_depth=3, nrounds=50, objective="multi:softmax")
pred=predict(bst, as.matrix(test[,1:561]))
mean(pred==test_label)*100
