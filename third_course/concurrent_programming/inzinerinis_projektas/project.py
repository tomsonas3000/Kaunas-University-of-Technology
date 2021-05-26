import numpy as np
import random
import time
from multiprocessing import Pool
from functools import partial

def distanceBetweenPoints(x1, y1, x2, y2): 
    #apskaičiuoja atstumą tarp dviejų taškų
    return ((x1-x2)**2 + (y1-y2)**2)**(0.5) 


def averageDistanceBetweenPoints(x): 
    #apskaičiuoja vidutinį atstumą tarp taškų
    avg = 0
    count = 0
    for i in range(0, len(x)):
        for j in range(i+1, len(x)):
            avg += distanceBetweenPoints(x[i][0], x[i][1], x[j][0],  x[j][1])
            count += 1
    return avg / count 

def target(x, n, m, S, lam):
    #tikslo funkcija
    distance = 0
    average = averageDistanceBetweenPoints(x)
    for i in range(0, len(x)): #sumuojamas kiekvieno taško nuokrypis nuo vidurkio
        for j in range(n, n+m):
            if(i == j):
                continue
            distance += abs(average - distanceBetweenPoints(x[j][0], x[j][1], x[i][0], x[i][1]))
    Sdistance = 0
    for i in range(n, n+m): #sumuojamas kiekvieno taško atstumas nuo koordinačių pradžios
        Sdistance += abs(S-distanceBetweenPoints(0, 0, x[i][0], x[i][1]))
    return (lam * distance) + (1-lam) * Sdistance


def gradientVector(i, x, targetFunc, S, lam, n, m, dx): 
    #Suskaičiuoja gradiento vektorių
    x1 = x[i]
    A = []
    for j in range(0, len(x1)):
        xx = x1[j]
        x[i][j] += dx
        dff = targetFunc(x, n, m, S, lam)
        x[i][j] = xx
        ff = targetFunc(x, n, m, S, lam)
        A.append((dff-ff)/dx)
    return A

def gradientMethod(step, itermax, eps, bounds, targetFunc, x, S, lam, dx, m,n): 
    #gradientinio nusileidimo metodas
    for _ in range(0, n): 
        #sugeneruoja atsitiktinius taškus
        x.append([random.uniform(bounds[0][0], bounds[0][1]), random.uniform(
            bounds[1][0], bounds[1][1])])
    #sugeneruoja papildomus taškus į koordinačių pradžią
    for _ in range(0, m): 
        x.append([0, 0])
    iteration = 0
    steps = [step]*m
    ffs = [0]*m
    while iteration < itermax:
        print(iteration)
        #nurodomas paleidžiamų procesų kiekis
        p = Pool(4)
        #apskaičiuojami visų taškų gradientai 
        grads = p.map(partial(gradientVector, x=x, targetFunc=targetFunc,
                              S=S, lam=lam, n=n, m=m, dx=dx), range(n, n+m))
        p.close()
        p.join()
        for i in range(n, n+m):
            j = i-n
            grad = grads[j]
            #sutvarkome gradientą taip, kad būtų nurodyto ilgio, bet nebūtų pakeista jo kryptis
            deltax = grad/np.linalg.norm(grad)*steps[j] 
            #"pastumiame" taškus priešinga gradientui kryptimi
            x[i] = (x[i]-deltax).tolist() 
            #apskaičiuojame tikslo funkciją
            ff = targetFunc(x, n, m, S, lam)
            if(ffs[j] < ff): #patikriname, ar funkcija pradėjo kilti
                steps[j] /= 1.5
            ffs[j] = ff
        iteration += 1
        accuracy = np.linalg.norm(steps) 
        #tikriname, ar nurodytas rezultatas tenkina mūsų nurodytą tikslumą
        if(accuracy < eps): 
            return x
    print("Pasiektas maksimalus iteracijų skaičius")
    return x    

start = time.time() #programos pradėjimo laikas
step = 1 #kitimo žingsnis
itermax = 100 #maksimalus iteracijų skaičius
eps = 1e-3 #siekiamas tikslumas
bounds = [[-10, 10], [-10, 10]] #ribos, kuriose bus generuojami pradiniai taškai
x = [] #pradinis rezultatų masyvas
S = 6 #reikšmė, nurodyta užduotyje
lam = 0.3 #pagalbinis parametras
dx = 0.01 #x pokytis skaičiuojant išvestines
m = 5 #papildomų taškų kiekis
n = 3 #pradinių taškų kiekis
if __name__ == "__main__": #reikalinga programos veikimui
    result = gradientMethod(step, itermax, eps, bounds,
                            target, x, S, lam, dx, m, n)

    print("n:\n", np.matrix(result[0:n]), "\nm:\n", np.matrix(result[n:n+m]))
    end = time.time() #programos užbaigimo laikas
    print("Užtruktas laikas")
    print(end-start)
