train = read.csv("activity_train.csv", header=TRUE)
test = read.csv("activity_test.csv", header=TRUE)
train$activity = as.factor(train$activity)
test$activity = as.factor(test$activity)
set.seed(1)

#train_numeric = train
#test_numeric = test

#for (i in 1:561) {
#  train_numeric[, i] = as.factor(train_numeric[, i])
#  test_numeric[, i] = as.factor(test_numeric[, i])
#  train_numeric[, i]=as.numeric(train_numeric[, i])
#  test_numeric[, i] = as.numeric(test_numeric[, i])
#}
pr.out = prcomp(train[, 1:561], scale=TRUE)
summarypr = summary(pr.out)
pr.out.sumamry[["importance"]][3,]

plot(summary(pr.out)$importance[3,], lwd=3, col="red", type="l", ylab="Cumulative fraction of explained variance", xlab="Number of PCs")


train_z = pr.out$x[,1:83]
prd = predict(pr.out, test)
test_z = prd[,1:83]
train_z=data.frame(train_z, activity=train$activity)
test_z=data.frame(test_z, activity=test$activity)

library(randomForest)
rf=randomForest(activity~.,data=train_z,ntree=10,importance=T, mtry=10)
pred=predict(rf,test_z)
mean(pred==test_z$activity)*100
confMat=table(true_lab=test_z$activity, predict=pred)

library(xgboost)
train_label=as.numeric(train$activity)-1
test_label=as.numeric(test$activity)-1
bst = xgboost(data = as.matrix(train[,1:83]), label=train_label, num_class=12, max_depth=20, nrounds=25, objective="multi:softmax")
pred=predict(bst, as.matrix(test[,1:83]))
mean(pred==test_label)*100

