import spacy
import sys

print(sys.argv[1])

nlp = spacy.load("en_core_web_sm")
if sys.argv[1] is None:
    print("No argument!")
    exit;
doc = nlp(sys.argv[1])

for token in doc:
    print("VALUE", token.i, token.pos_)
print("PROCESS DONE");


