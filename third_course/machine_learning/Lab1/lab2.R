idx.train = createDataPartition(y=Spotify$target,p=0.8,list=FALSE)
train = Spotify[idx.train, ]
test = Spotify[-idx.train, ]
glm.fits=glm(target~danceability+duration_ms+loudness+valence,family=binomial,data=train)
glm.fits=glm(target~.-X-song_title-artist, family=binomial, data=Spotify)
glm.probs=predict(glm.fits,test,type="response")
glm.pred=rep("0",403)
glm.pred[glm.probs > .5] = "1"
table(true_classes=test$target,predicted=glm.pred)
mean(glm.pred==test$target)
