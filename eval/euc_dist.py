#!/usr/bin/env python
import pandas as pd
import numpy as np

positions = ["PosX", "PosY", "PosZ"]
rotations = ["RotX", "RotY", "RotZ"]
tmp = positions + rotations

df = pd.read_csv("UserID01561734104638.csv", delimiter=";")

df = df[(df.isStudy == True)]
#print(df.dtypes)


def calc_euc(df):
    for pos in positions:
        df[pos + "_diff"] = df[pos] - df[pos].shift(1)
        df[pos + "_square"] = df[pos + "_diff"] * df[pos + "_diff"]

    df["euc"] = (df["PosX_square"] + df["PosY_square"] + df["PosZ_square"]).apply(lambda x: np.sqrt(x))
    return df["euc"].sum()


for i in range(1, 4):
    for hand in ["Left", "Right"]:
        tmp = df[(df.Hand == hand) & (df.Condition == i)]
        print("condition", str(i), "-", hand, ":\t", calc_euc(tmp))
