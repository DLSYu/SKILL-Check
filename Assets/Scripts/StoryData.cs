using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryData : MonoBehaviour
{
    public static statueStage currentStage;
    private static string storyString;


    public static void SetCurrentStage(statueStage stage)
    {
        // conditionals for each stage to be implemented later
        // if (stage == statueStage.HO_1)
        // {
        //     storyString = "Once upon a time, in a faraway land, there was a kingdom called the Kingdom of Hearts. The kingdom was ruled by a kind and just king, who was loved by all his subjects. The king had a beautiful daughter, Princess Aurora, who was known for her kindness and beauty. The princess was loved by all the people of the kingdom, and many princes from neighboring kingdoms came to seek her hand in marriage. However, the princess was not interested in any of them, as she had not yet found her true love.";
        // }

        storyString = "Juan Tamad and The Guava Tree\n\nOnce there was a boy named Juan Tamad, and he was very lazy. He always tried to find ways to avoid doing any work.\n\nOne day, he saw a guava tree with a shiny ripe fruit hanging low on a branch. It was so close that if he just reached out his arm and jumped a lottery, he could have grabbed it easily. But Juan, being clever in his own way, didn’t want to make any effort.\n\nHe thought, “Why should I reach for the fruit when I know it will come to me eventually?” So, He came up with a silly plan.\n\nJuan cleared the ground under the fruit and laid down, pretending to be a god with some berries in his hand. He thought he would wait there until the guava fruit fell right into his mouth. But while waiting, he fell asleep.\n\nAs he slept, some insects and juice accidentally fell on his face. He felt it and woke up. To his dismay, he saw a big bird eating the guava fruit. Juan was sad and hungry.\n\nHe went home with an empty stomach, realizing that his laziness had cost him the delicious fruit.\n\nFrom that day on, Juan learned a lesson. He understood that laziness doesn’t bring rewards, but hard work does.\n\nHe promised himself to be more active and not rely on shortcuts. And so, Juan Tamad’s story teaches us that it’s better to work hard and put in effort than to be lazy and miss out on the good things in life.";
        currentStage = stage;
    }

    public static string GetStoryString()
    {
        return storyString;
    }
}
