using UnityEngine;

public class InTextDefinition
{
    public string definition;
    public string example;
    public bool exists;
    public InTextDefinition(string definition, string example, bool exists = true)
    {
        this.definition = definition;
        this.example = example;
        this.exists = exists;
    }

    public override string ToString()
    {
        return $"Definition: {definition}\nExample: {example}";
    }
}