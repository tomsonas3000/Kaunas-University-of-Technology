library(h2o)
h2o.init(nthreads = -1)

train = read.csv("arrhythmia_train.csv")
test = read.csv("arrhythmia_test.csv")
train$arrhythmia = as.factor(train$arrhythmia)
test$arrhythmia = as.factor(test$arrhythmia)
levels(train$arrhythmia) = c("normal", "supraventricular", "ventricular","unknown")
levels(test$arrhythmia) = c("normal", "supraventricular", "ventricular","unknown")

#1
par(mfrow=c(2,5))
for (i in c(1:10)) {
  y=as.vector(train[i,1:187])
  x=as.vector(c(1:187))
  plot(x,y, type='l')
}

#2
par(mfrow=c(1,1))
plot(train$X10, train$X20, col=(train$arrhythmia), pch=16)

#3
print(table(train$arrhythmia)) 
print(table(test$arrhythmia)) 

#4
h2o_train = as.h2o(train)
h2o_test = as.h2o(test)
dl_model_1 = h2o.deeplearning(x=names(h2o_train[,1:187]), y = "arrhythmia",
                            training_frame = h2o_train,
                            activation="Tanh",
                            hidden = c(15),
                            loss = "CrossEntropy",
                            score_each_iteration=TRUE,
                            epochs = 1000,
                            balance_classes=F,
                            rate=0.01,
                            adaptive_rate = F,
                            stopping_rounds=0,
                            l1=0,
                            l2=0.01)
plot(dl_model_1)
prediction = as.data.frame(h2o.predict(object = dl_model_1,newdata = h2o_test))
mean(prediction$predict==test$arrhythmia)
confMatrix=table(predicted_labels=prediction$predict,true_labels=test$arrhythmia)
temp=confMatrix[3,]
confMatrix[3,]=confMatrix[4,]
confMatrix[4,]=temp
confMatrix
diag(confMatrix)/rowSums(confMatrix)*100

dl_model_2 = h2o.deeplearning(x=names(h2o_train[,1:187]), y = "arrhythmia",
                              training_frame = h2o_train,
                              activation="Rectifier",
                              hidden = c(15),
                              loss = "CrossEntropy",
                              score_each_iteration=TRUE,
                              epochs = 500,
                              balance_classes=F,
                              rate=0.01,
                              adaptive_rate = F,
                              stopping_rounds=0,
                              l1=0,
                              l2=0.01)
plot(dl_model_2)
prediction = as.data.frame(h2o.predict(object = dl_model_2,newdata = h2o_test))
mean(prediction$predict==test$arrhythmia)
confMatrix=table(predicted_labels=prediction$predict,true_labels=test$arrhythmia)
temp=confMatrix[3,]
confMatrix[3,]=confMatrix[4,]
confMatrix[4,]=temp
confMatrix
diag(confMatrix)/rowSums(confMatrix)*100

dl_model_3 = h2o.deeplearning(x=names(h2o_train[,1:187]), y = "arrhythmia",
                              training_frame = h2o_train,
                              activation="Rectifier",
                              hidden = c(12,12),
                              loss = "CrossEntropy",
                              score_each_iteration=TRUE,
                              epochs = 500,
                              balance_classes=F,
                              rate=0.01,
                              adaptive_rate = F,
                              stopping_rounds=0,
                              l1=0,
                              l2=0.01)
plot(dl_model_3)
prediction = as.data.frame(h2o.predict(object = dl_model_3,newdata = h2o_test))
mean(prediction$predict==test$arrhythmia)
confMatrix=table(predicted_labels=prediction$predict,true_labels=test$arrhythmia)
temp=confMatrix[3,]
confMatrix[3,]=confMatrix[4,]
confMatrix[4,]=temp
confMatrix
diag(confMatrix)/rowSums(confMatrix)*100

dl_model_4 = h2o.deeplearning(x=names(h2o_train[,1:187]), y = "arrhythmia",
                              training_frame = h2o_train,
                              activation="Maxout",
                              hidden = c(15,15,15),
                              loss = "CrossEntropy",
                              score_each_iteration=TRUE,
                              epochs = 500,
                              balance_classes=F,
                              rate=0.001,
                              adaptive_rate = F,
                              stopping_rounds=0,
                              l1=0,
                              l2=0.01)
plot(dl_model_4)
prediction = as.data.frame(h2o.predict(object = dl_model_4,newdata = h2o_test))
mean(prediction$predict==test$arrhythmia)
confMatrix=table(predicted_labels=prediction$predict,true_labels=test$arrhythmia)
temp=confMatrix[3,]
confMatrix[3,]=confMatrix[4,]
confMatrix[4,]=temp
confMatrix
diag(confMatrix)/rowSums(confMatrix)*100

dl_model_5 = h2o.deeplearning(x=names(h2o_train[,1:187]), y = "arrhythmia",
                              training_frame = h2o_train,
                              activation="Maxout",
                              hidden = c(15,15,15),
                              loss = "CrossEntropy",
                              score_each_iteration=TRUE,
                              epochs = 10,
                              balance_classes=F,
                              rate=0.001,
                              adaptive_rate = F,
                              stopping_rounds=0,
                              l1=0,
                              l2=0.01)
plot(dl_model_5)
prediction = as.data.frame(h2o.predict(object = dl_model_5,newdata = h2o_test))
mean(prediction$predict==test$arrhythmia)
confMatrix=table(predicted_labels=prediction$predict,true_labels=test$arrhythmia)
temp=confMatrix[3,]
confMatrix[3,]=confMatrix[4,]
confMatrix[4,]=temp
confMatrix
diag(confMatrix)/rowSums(confMatrix)*100



