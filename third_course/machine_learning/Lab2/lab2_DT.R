#1a
Spotify = read.csv("music_spotify.csv", header=TRUE)
library(caret)
library(rpart)
library(rpart.plot)
idx.train = createDataPartition(y = Spotify$target, p = 0.8, list = FALSE)
train = Spotify[idx.train,]
test = Spotify[-idx.train,]
set.seed(1)
cfit = rpart(target ~ ., train[2:15], method="class", control=rpart.control(cp=0))
printcp(cfit)
plotcp(cfit)
#1b
cfit_pruned = prune(cfit, cp=0.00620347)
rpart.plot(cfit_pruned, fallen.leaves = FALSE, tweak=1.4, extra=101, type=1, box.palette = "YlGnBl")
#1c
sum(cfit_pruned$frame$var == "<leaf>")
printcp(cfit_pruned)
#1e
pred = predict(cfit, newdata=test, type="class")
mean(pred==test$target)
table(true_classes=test$target, predicted=pred)

pred_pruned = predict(cfit_pruned, newdata=test, type="class")
mean(pred_pruned==test$target)
table(true_classes=test$target, predicted=pred_pruned)
#1g
barplot(cfit_pruned$variable.importance)

#2
train_sign = read.csv("sign_mnist_train.csv", header=TRUE)
test_sign = read.csv("sign_mnist_test.csv", header=TRUE)
set.seed(1)
cfit_sign = rpart(label ~ .,train_sign, method="class", control = rpart.control(cp=0))
#2a
printcp(cfit_sign)
plotcp(cfit_sign)
#2b
cfit_sign_pruned = prune(cfit_sign, c = 0)
rpart.plot(cfit_sign_pruned, fallen.leaves = FALSE, tweak=1.2, extra=101, type=1, box.palette = "YlGnBl")
sum(cfit_sign_pruned$frame$var == "<leaf>")
printcp(cfit_sign_pruned)
#3b
pred_sign = predict(cfit_sign, newdata=test_sign, type="class")
mean(pred_sign==test_sign$label)

pred_pruned_sign = predict(cfit_sign_pruned, newdata=test_sign, type="class")
mean(pred_pruned_sign==test_sign$label)
#3d
train_skip = train_sign[,c(1,seq(2,785,4))]
test_skip = test_sign[,c(1,seq(2,785,4))]
cfit_skip = rpart(label ~ .,train_skip, method="class", control = rpart.control(cp=0))
pred_skip = predict(cfit_skip, newdata=test_skip, type="class")
mean(pred_skip==test_skip$label)
