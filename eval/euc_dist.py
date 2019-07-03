#!/usr/bin/env python
import pandas as pd
import numpy as np
import glob

positions = ["PosX", "PosY", "PosZ"]
rotations = ["RotX", "RotY", "RotZ"]
tmp = positions + rotations


import pandas as pd


path = r'C:\Users\janle\Google Drive\VR_Study_CSV\CSV' # use your path
all_files = glob.glob(path + "/*.csv")

li = []

for filename in all_files:
    df = pd.read_csv(filename, delimiter=";")
    li.append(df)




#print(df.dtypes)


def calc_euc(df):
    for pos in positions:
        df[pos + "_diff"] = df[pos] - df[pos].shift(1)
        df[pos + "_square"] = df[pos + "_diff"] * df[pos + "_diff"]

    df["euc"] = (df["PosX_square"] + df["PosY_square"] + df["PosZ_square"]).apply(lambda x: np.sqrt(x))
    return df["euc"].sum()

for j in range(len(li)):
    df = li[j]
    df = df[(df.isStudy == True)]
    print(j)
    for i in range(1, 4):
        for hand in ["Left", "Right"]:
            tmp = df[(df.Hand == hand) & (df.Condition == i)]
            print("condition", str(i), "-", hand, ":\t", calc_euc(tmp))
