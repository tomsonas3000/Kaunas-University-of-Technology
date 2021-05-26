import numpy as np
import random
import matplotlib
import time
from multiprocessing import Pool


def dist(x1, y1, x2, y2): #atstumas tarp dvieju tasku
    return ((x1-x2)**2 + (y1-y2)**2)**(0.5) 


def avg(x): #vidutinis atstumas tarp visu tasku
    avg = 0
    count = 0
    for i in range(0, len(x)):
        for j in range(i+1, len(x)):
            avg += dist(x[i][0], x[i][1], x[j][0],  x[j][1])
            count += 1
    return avg / count 

def target(x, n, m, S, lam): #tikslo funkcija
    distance = 0
    average = avg(x)
    for i in range(0, len(x)): #sumuoja kiekvieno tasko nuokrypi nuo vidurkio
        for j in range(n, n+m):
            if(i == j):
                continue
            distance += abs(average - dist(x[j][0], x[j][1], x[i][0], x[i][1]))
    
    Sdistance = 0
    for i in range(n, n+m): #sumuoja kiekvieno tasko atstuma nuo koordinaciu pradzios
        Sdistance += abs(S-dist(0, 0, x[i][0], x[i][1]))
    return (lam * distance) + (1-lam) * Sdistance


def gradVec(x, x1, targetFunc, S, lam, n, m, i, dx): #padaro gradiento vektoriu
    A = []
    for j in range(0, len(x1)):
        xx = x1[j]
        x[i][j] += dx
        dff = targetFunc(x, n, m, S, lam)
        x[i][j] = xx
        ff = targetFunc(x, n, m, S, lam)
        A.append((dff-ff)/dx)
    return A


def gradientMethod(step, itermax, eps, bounds, targetFunc, x, S, lam, dx, m,n): #gradientinio nusileidimo metodas
    for _ in range(0, n):
        x.append([random.uniform(bounds[0][0], bounds[0][1]), random.uniform(
            bounds[1][0], bounds[1][1])])
    for _ in range(0, m): #sugeneruoja m taskus i koordinaciu pradzia
        x.append([0, 0])

    iteration = 0
    steps = [step]*m
    ffs = [0]*m
    while iteration < itermax:
        print(iteration)
        for i in range(n, n+m):
            j = i-n
            grad = gradVec(x, x[i], targetFunc, S, lam, n, m, i, dx) #gradiento vektorius
            deltax = grad/np.linalg.norm(grad)*steps[j] #krypties vektorius
            x[i] = (x[i]-deltax).tolist() #pastumiu taska
            ff = targetFunc(x, n, m, S, lam) #tikslo funkcija
            if(ffs[j] < ff): #tikrina, ar funkcija pradejo kilt
                steps[j] /= 1.5
            ffs[j] = ff
        iteration += 1
        accuracy = np.linalg.norm(steps)
        if(accuracy < eps):
            return x
    return x    

step = 1
itermax = 100
eps = 1e-3
bounds = [[-10, 10], [-10, 10]]
x = []
S = 6
lam = 0.2
dx = 0.01
m = 65
n = 50
start = time.time()
result = gradientMethod(step, itermax, eps, bounds,
                        target, x, S, lam, dx, m, n)

print("n:\n", np.matrix(result[0:n]), "\nm:\n", np.matrix(result[n:n+m]))    
print("Užtruktas laikas")
end = time.time()
print(end-start)