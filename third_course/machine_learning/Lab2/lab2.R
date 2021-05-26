train = read.csv("sign_mnist_train.csv", header=TRUE)
test = read.csv("sign_mnist_test.csv", header=TRUE)

par(mfrow=c(1,1), tcl=-0.5, mai = c(0.1,0.1,0.1,0.1), xaxt='n', yaxt='n')
for (i in c(1)) {
  hand_sign = as.matrix(train[i,2:785])
  image(matrix(hand_sign,28,28,byrow=F), col = gray.colors(255))
}

#1
par(mfrow=c(4,4), tcl=-0.5, mai = c(0.1,0.1,0.1,0.1), xaxt='n', yaxt='n')
id_letter=which(train$label==0)
for (i in id_letter[1:16]){
  hand_sign = as.matrix(train[i,2:785])
  image(matrix(hand_sign, 28,28, byrow=F), col=gray.colors(255))
}
#2
train = read.csv("sign_mnist_train.csv", header=TRUE)
test = read.csv("sign_mnist_test.csv", header=TRUE)

lda_classifier = lda (formula = label ~., data = train)
predictions = predict(lda_classifier,test)$class
confMat = table(predicted=predictions,true_labels=test$label)
confMat
mean(test$label == predictions)
accuracy_for_each=diag(confMat)/colSums(confMat)
barplot(accuracy_for_each, main="Accuracy for each class", xlab="letter number", ylab="accuracy")

#3
half_train = train[,c(1,seq(2,785,4))]
half_test = test[,c(1,seq(2,785,4))]
lda_classifier_half = lda(formula = label ~., data=half_train)
predictions = predict(lda_classifier_half,half_test)$class
mean(half_test$label == predictions)
#4
qda_classifier = qda(formula = label ~., data = train)
predictions = predict(qda_classifier, test)$class
confMat = table(predicted=predictions,true_labels=test$label)
confMat
mean(test$label == predictions)
accuracy_for_each=diag(confMat)/colSums(confMat)
barplot(accuracy_for_each, main="Accuracy for each class", xlab="letter number", ylab="accuracy")
#5
half_train = train[,c(1,seq(2,785,5))]
half_test = test[,c(1,seq(2,785,5))]
qda_classifier_half = qda(formula = label ~., data=half_train)
predictions = predict(qda_classifier_half,half_test)$class
mean(half_test$label == predictions)

