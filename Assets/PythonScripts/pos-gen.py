import spacy
import sys

print(sys.argv[1])

nlp = spacy.load("en_core_web_sm")
if sys.argv[1] is None:
    print("No argument!")
    exit;
doc = nlp(sys.argv[1])
print("VALUE", doc[0].pos_, end=" ")
for token in doc[1:]:
    print(token.pos_, end=" ")
print('\n')
print("PROCESS DONE");


