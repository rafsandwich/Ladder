# pokemon game mockup written up on a whim after my partner suggested it

import random

class Lad:
    def __init__(self, name, hp, spd, move1, move2):
        self.name = name
        self.hp = hp
        self.spd = spd
        #self.atk = atk
        self.moves = {
            "Move1": move1,
            "Move2": move2
        }

    def attack(self, other, move=None):
        if move==None:
            move = random.choice(list(self.moves.keys()))
        damage = self.moves[move]
        other.hp -= damage

        print(f"{self.name} used {move} on {other.name}, hitting for {damage} damage!")
        print(f"{other.name} | HP: {other.hp}")

    def is_dead(self):
        return self.hp <= 0

    def __str__(self):
        return f"{self.name} | HP = {self.hp}, Speed = {self.spd}, Moves = {self.moves}"

lad1 = Lad(name="Fish", hp=15, spd=10, move1=13, move2=6)
lad2 = Lad(name="Seagull", hp=17, spd=9, move1=12, move2=9)
lad3 = Lad(name="Dirt", hp=35, spd=2, move1=5, move2=10)
lad4 = Lad(name="Bed", hp=40, spd=1, move1=4, move2=7)
lad5 = Lad(name="Jess", hp=22, spd=5, move1=8, move2=11)
lad6 = Lad(name="Raph", hp=23, spd=8, move1=6, move2=12)

print(lad1)
print(lad2)

while lad1.hp >= 0 and lad2.hp >= 0:
    if lad1.spd > lad2.spd:

        move_choice = input(f"What move for {lad1.name} | (Move1 / Move2): ")
        while move_choice not in lad1.moves:
            print("Invalid choice! Please choose Move1 / Move2: ")
            move_choice = input(f"What move for {lad1.name} | (Move1 / Move2): ")                    
                            
        lad1.attack(lad2, move_choice)
        if not lad2.is_dead():
            lad2.attack(lad1)

    else:
        lad2.attack(lad1)
        if not lad1.is_dead():

            move_choice = input(f"What move for {lad1.name} | (Move1 / Move2): ")
            while move_choice not in lad1.moves:
                print("Invalid choice! Please choose Move1 / Move2: ")
                move_choice = input(f"What move for {lad1.name} | (Move1 / Move2): ")                    
        
            lad1.attack(lad2, move_choice)

if lad1.hp <= 0:
    print(lad2.name + " wins!")
elif lad2.hp <=0:
    print(lad1.name + " wins!")
