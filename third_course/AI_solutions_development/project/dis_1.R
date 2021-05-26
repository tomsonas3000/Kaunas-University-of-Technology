library(h2o)
library(e1071)
h2o.init(nthreads = -1)

data = read.csv("phpg2t68G.csv")
data$attribute_21 = as.factor(data$attribute_21)
levels(data$attribute_21) = c("true", "false")

library(caret)
idx.train = createDataPartition(y = data$attribute_21, p = 0.8, list = FALSE)
train = data[idx.train,]
test = data[-idx.train,]

h2o_train = as.h2o(train)
h2o_test = as.h2o(test)

names(h2o_train[, 1:21])

dl_model = h2o.deeplearning(
  x = names(h2o_train[, 1:21]),
  y = "attribute_21",
  training_frame = h2o_train,
  activation="Rectifier",
  hidden = c(20),
  loss = "CrossEntropy",
  score_each_iteration = TRUE,
  epochs = 100,
  rate = 0.00001,
  balance_classes = F,
  adaptive_rate = F,
  stopping_rounds = 0,
  l1=0,
  l2=0.01)
plot(dl_model)

prediction = as.data.frame(h2o.predict(object = dl_model, newdata = h2o_test))
mean(prediction$predict==test$attribute_21)
confMatrix=table(predicted_labels=prediction$predict,true_labels=test$attribute_21)
diag(confMatrix)/rowSums(confMatrix)*100

glm.fits=glm(attribute_21~.,family=binomial,data=train)
glm.probs=predict(glm.fits,test,type="response")
glm.pred=rep("0",19263)
glm.pred[glm.probs > .5] = "1"
table(true_classes=test$attribute_21,predicted=glm.pred)
mean(glm.pred==test$attribute_21)



