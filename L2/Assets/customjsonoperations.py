# -*- coding: utf-8 -*-
"""
custom json operations
"""
import json

print("hey, it's me - Python!")

f = open('data2.json', )
data = json.load(f)

example_stat = 0
for x in data['buses']:
    if x['udogodnienia']['klimatyzacja'] == 'nie' and x['danePodstawowe']['rok_produkcji'] is not None and int(x['danePodstawowe']['rok_produkcji']) > 2010:
        example_stat += 1

print(f'nowe busy bez klimatyzacji: {str(example_stat)}')


def serializeBus(bus):
    return {
        "nr_inwentarzowy": bus['nr_inwentarzowy'],
        "rodzaj_pojazdu": bus['danePodstawowe']['rodzaj_pojazdu'],
        "typ_pojazdu": bus['danePodstawowe']['typ_pojazdu'],
        "rok_produkcji": bus['danePodstawowe']['rok_produkcji'],
        "liczba_miejsc_stojacych": bus['danePodstawowe']['liczba_miejsc_siedzacych'],
        "liczba_miejsc_siedzacych": bus['danePodstawowe']['liczba_miejsc_stojacych'],
        "mocowanie_rowerow": bus['udogodnienia']['mocowanie_rowerow']
    }

def isBusRelevant(bus):
    return bus['danePodstawowe']['marka'].lower() == 'mercedes'\
           and bus['udogodnienia']['AED'].lower() == 'tak'\
           and bus['udogodnienia']['monitoring'].lower() == 'tak'


jsontemp = {"buses": [serializeBus(bus) for bus in data['buses'] if isBusRelevant(bus)]}

with open('data3.json', 'w', encoding='utf-8') as f:
    json.dump(jsontemp, f)
