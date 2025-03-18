
public class Definitions
{
    public string noun { get; set; }
    public string verb { get; set; }
    public string adjective { get; set; }
    public string adverb { get; set; }
    public string conjunction { get; set; }
    public string exclamation { get; set; }
    public string interjection { get; set; }
    public string pronoun { get; set; }
    public string preposition { get; set; }
}

public enum POS
{
    PROPN, NOUN, VERB, ADJ, ADV, CONJ, INTJ, PRON, NONE
}