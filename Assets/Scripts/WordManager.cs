﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    
    public WordSpawner wordSpawner;
    public int minWords;
    public int numWordsToPile;
    [SerializeField]
    private int numWordsPiled = 0;
    SortedList<string, GameObject> activeWords;

    private readonly string[] wordList_Sad = { "abysmal", "adverse", "alarming", "angry", "annoy", "anxious", "apathy", "appalling", "atrocious", "awful", "bad", "banal", "barbed", "belligerent", "bemoan", "beneath", "boring", "broken", "callous", "clumsy", "coarse", "cold", "collapse", "confused", "contradictory", "contrary", "corrosive", "corrupt", "crazy", "creepy", "criminal", "cruel", "cry", "cutting", "damage", "damaging", "dastardly", "dead", "decaying", "deformed", "deny", "deplorable", "depressed", "deprived", "despicable", "detrimental", "dirty", "disease", "disgusting", "disheveled", "dishonest", "dishonorable", "dismal", "distress", "dreadful", "dreary", "enraged", "eroding", "evil", "fail", "faulty", "fear", "feeble", "fight", "filthy", "foul", "frighten", "frightful", "gawky", "ghastly", "grave", "greed", "grim", "grimace", "gross", "grotesque", "gruesome", "guilty", "haggard", "hard", "harmful", "hate", "hideous", "homely", "horrendous", "horrible", "hostile", "hurt", "hurtful", "icky", "ignorant", "ignore", "ill", "immature", "imperfect", "impossible", "inane", "inelegant", "infernal", "injure", "injurious", "insane", "insidious", "insipid", "jealous", "junky", "lose", "lousy", "lumpy", "malicious", "mean", "menacing", "messy", "misshapen", "missing", "misunderstood", "moan", "moldy", "monstrous", "naive", "nasty", "naughty", "negate", "negative", "never", "no", "nobody", "nondescript", "nonsense", "not", "noxious", "objectionable", "odious", "offensive", "old", "oppressive", "pain", "perturb", "pessimistic", "petty", "plain", "poisonous", "poor", "prejudice", "questionable", "quirky", "quit", "reject", "renege", "repellant", "reptilian", "repugnant", "repulsive", "revenge", "revolting", "rocky", "rotten", "rude", "ruthless", "sad", "savage", "scare", "scary", "scream", "severe", "shocking", "shoddy", "sick", "sickening", "sinister", "slimy", "smelly", "sobbing", "sorry", "spiteful", "sticky", "stinky", "stormy", "stressful", "stuck", "stupid", "substandard", "suspect", "suspicious", "tense", "terrible", "terrifying", "threatening", "ugly", "undermine", "unfair", "unfavorable", "unhappy", "unhealthy", "unjust", "unlucky", "unpleasant", "unsatisfactory", "unsightly", "untoward", "unwanted", "unwelcome", "unwholesome", "unwieldy", "unwise", "upset", "vice", "vicious", "vile", "villainous", "vindictive", "wary", "weary", "wicked", "woeful", "worthless", "wound", "yell", "yucky", "zero" };
   
    // Start is called before the first frame update
    void Start()
    {
        activeWords = new SortedList<string, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (numWordsPiled < numWordsToPile)
        {
            string word = getRandWord();
            while (activeWords.ContainsKey(word)) word = getRandWord();
            activeWords.Add(word,wordSpawner.SpawnWord(word));
            numWordsPiled++;
        }
        else if(activeWords.Count < minWords)
        {
            numWordsPiled = 0;
        }
    }

    private string getRandWord()
    {
        return wordList_Sad[Random.Range(0, wordList_Sad.Length)];
    }

    void OnDestroy()
    {
        activeWords.Clear();
    }

    public void SubmitWord(string word)
    {
        GameObject wordTyped = activeWords[word];
        if (wordTyped != null)
        {
            activeWords.Remove(word);
            Destroy(wordTyped);
        }
    }
}
