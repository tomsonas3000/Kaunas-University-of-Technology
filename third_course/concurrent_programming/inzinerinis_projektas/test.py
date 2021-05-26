from multiprocessing.dummy import Pool as ThreadPool
import numpy as np 

def write(i, x):
    print(i, "---", x)

a = ["1","2","3"]
b = ["4","5","6"]

c = [["1","2","3"], ["4","5","6"]]
print(zip(np.array_split(c,2)))

pool = ThreadPool(2)
pool.starmap(write, zip(np.array_split(c,2))) 
pool.close() 
pool.join()