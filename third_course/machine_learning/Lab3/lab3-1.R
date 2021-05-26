train = read.csv("satellite_train.csv", header=TRUE)
test = read.csv("satellite_test.csv", header=TRUE)

train$V37 = as.factor(train$V37)
test$V37 = as.factor(test$V37)

levels(train$V37) = c ("red soil", "cotton crop", "grey soil","damp grey soil","soil with vegetation","very damp grey soil")
levels(test$V37) = c ("red soil", "cotton crop", "grey soil","damp grey soil","soil with vegetation","very damp grey soil")

#1
plot(train$V37, ylab="Frequency", xlab="Class name")

#2
plot(train$V10, train$V20, col=(train$V37), pch=16)

#3
set.seed(1)
tune.out = tune(svm, train$V37~train$V10+train$V20, data=train, kernel="linear", ranges=list(cost=c(0.001,0.1,1,5,10,100)))

summary(tune.out)
dat=data.frame(V1=train$V10,V2=train$V20,y=train$V37)
svmfit = svm(dat$y ~., data=dat, kernel="linear", cost=10, scale=TRUE)
plot(svmfit,dat,grid=100,color.palette=terrain.colors)
test_dat=data.frame(V1=test$V10,V2=test$V20,y=test$V37)
pred=predict(svmfit, newdata=test_dat)
confusionTable = table(predicted_labels=pred, true_labels=test_dat$y)
accuracy_for_each=diag(confusionTable)/colSums(confusionTable)
acc = 1 - accuracy_for_each
acc
mean(test_dat$y == pred)

#4
set.seed(1)
tune.out.nonlinear = tune(svm, train$V37~train$V10+train$V20, data=train, kernel="radial", ranges=list(cost=c(0.001,1,5), gamma=c(0.0001,0.001,0.01)))
summary(tune.out.nonlinear)
svmfitnonlinear = svm(dat$y ~., data=dat, kernel="radial", cost=15, gamma=4, scale=TRUE)
plot(svmfitnonlinear,dat,grid=100,color.palette=terrain.colors)
prednonlinear=predict(svmfitnonlinear, newdata=test_dat)
confusionTableNonLinear = table(predicted_labels=prednonlinear, true_labels=test_dat$y)
confusionTableNonLinear
accuracy_for_each_non_linear=diag(confusionTableNonLinear)/colSums(confusionTableNonLinear)
acc_non_linear = 1 - accuracy_for_each_non_linear
acc_non_linear
mean(test_dat$y == prednonlinear)

#5
set.seed(1)
trainWV37 = train[-37]
dat_full=data.frame(trainWV37, y=train$V37)
testWV37 = test[-37]
test_dat_full=data.frame(testWV37, y=test$V37)

tune.out = tune(svm, V37~., data=train, kernel="linear", ranges=list(cost=c(0.001,0.1,1,5)))
summary(tune.out)
svmfit = svm(dat_full$y ~., data=dat_full, kernel="linear", cost=1, scale=TRUE)
pred=predict(svmfit, newdata=test_dat_full)
confusionTable = table(predicted_labels=pred, true_labels=test_dat_full$y)
confusionTable
accuracy_for_each=diag(confusionTable)/colSums(confusionTable)
acc = 1 - accuracy_for_each
acc
mean(test_dat$y == pred)

tune.out = tune(svm, V37~., data=train, kernel="radial", ranges=list(cost=c(0.001,0.1,1,5), gamma=c(0.001,0.01,0.1,1,2,3)))
summary(tune.out)
svmfit = svm(dat_full$y ~., data=dat_full, kernel="radial", cost=10, gamma=0.5, scale=TRUE)
pred=predict(svmfit, newdata=test_dat_full)
confusionTable = table(predicted_labels=pred, true_labels=test_dat_full$y)
confusionTable
accuracy_for_each=diag(confusionTable)/colSums(confusionTable)
acc = 1 - accuracy_for_each
acc
summary(svmfit)
mean(test_dat$y == pred)
tune.baisus = tune.out
summary(svmfit)
