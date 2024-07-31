# hi, this is a pokemon game mock up

class Lad:
    def __init__(self, name, hp, spd, atk):
        self.name = name
        self.hp = hp
        self.spd = spd
        self.atk = atk

    def attack(self, other):
        damage = self.atk
        other.hp -= damage

        print(f"{self.name} attacked {other.name}, hitting for {damage} damage!")
        print(f"{other.name} | HP: {other.hp}")

    def is_dead(self):
        return self.hp <= 0

    def __str__(self):
        return f"{self.name} | HP = {self.hp}, Speed = {self.spd}, Attack = {self.atk}"

lad1 = Lad(name="Fish", hp=15, spd=10, atk=10)
lad2 = Lad(name="Jess", hp=22, spd=5, atk=8)

print(lad1)
print(lad2)

while lad1.hp >= 0 and lad2.hp >= 0:
    if lad1.spd > lad2.spd:
        lad1.attack(lad2)
        if not lad2.is_dead():
            lad2.attack(lad1)

    else:
        lad2.attack(lad1)
        if not lad1.is_dead():
            lad1.attack(lad2)

if lad1.hp <= 0:
    print(lad2.name + " wins!")
elif lad2.hp <=0:
    print(lad1.name + " wins!")
