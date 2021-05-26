import pandas as pd
import seaborn as sns
from keras.models import Sequential
from keras.layers import Dense
sns.set_theme()

df = pd.read_csv("./Dataset/dataset-of-80s_broken.csv")

df['duration_ms'] = df['duration_ms'].apply(lambda x : x/1000)
df['duration_ms'] = df['duration_ms'].round(0)
df['duration_ms'] = df['duration_ms'].apply(lambda x : x / 60)

df.loc[(df['duration_ms'] <= 2), 'duration_ms'] = 0
df.loc[((df['duration_ms'] > 2) & (df['duration_ms'] <= 3)), 'duration_ms'] = 1
df.loc[((df['duration_ms'] > 3) & (df['duration_ms'] <= 4)), 'duration_ms'] = 2
df.loc[((df['duration_ms'] > 4) & (df['duration_ms'] <= 5)), 'duration_ms'] = 3
df.loc[(df['duration_ms'] > 5), 'duration_ms'] = 4

df.loc[(df['sections'] <= 5), 'sections'] = 0
df.loc[((df['sections'] > 5) & (df['sections'] <= 10)), 'sections'] = 1
df.loc[((df['sections'] > 10) & (df['sections'] <= 15)), 'sections'] = 2
df.loc[((df['sections'] > 15) & (df['sections'] <= 20)), 'sections'] = 3
df.loc[(df['sections'] > 20), 'sections'] = 4

df = df.drop(columns=['track', 'artist', 'uri'])

X = df.iloc[:, 0:15]
Y = df['target']

model = Sequential()
print(len(X))
model.add(Dense(12, input_dim=8, activation='relu'))

print(Y)
print(df.columns)

