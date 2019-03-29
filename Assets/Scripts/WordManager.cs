using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CorrectWordEvent : UnityEvent<int>
{
}
public class WordManager : MonoBehaviour
{
    
    public CorrectWordEvent correctWordEvent = new CorrectWordEvent();
    public UnityEvent wrongWordEvent = new UnityEvent();

    public WordSpawner wordSpawner;
    public int minWords;
    public int numWordsToPile;
    [SerializeField]
    private int numWordsPiled = 0;
    SortedList<string, GameObject> activeWords;

    private readonly string[] wordList_Sad = { "bad", "cry", "ill", "not", "old", "sad", "cold", "dead", "deny", "evil", "fail", "fear", "foul", "grim", "hard", "hate", "hurt", "icky", "lose", "mean", "moan", "pain", "poor", "quit", "rude", "sick", "ugly", "vice", "vile", "wary", "yell", "zero", "angry", "annoy", "awful", "banal", "crazy", "cruel", "dirty", "fight", "gawky", "grave", "greed", "gross", "inane", "junky", "lousy", "lumpy", "messy", "moldy", "naive", "nasty", "never", "petty", "plain", "rocky", "scare", "scary", "slimy", "sorry", "stuck", "tense", "upset", "weary", "wound", "yucky", "apathy", "barbed", "bemoan", "boring", "broken", "clumsy", "coarse", "creepy", "damage", "dismal", "dreary", "faulty", "feeble", "filthy", "guilty", "homely", "ignore", "injure", "insane", "negate", "nobody", "odious", "quirky", "reject", "renege", "rotten", "savage", "scream", "severe", "shoddy", "smelly", "sticky", "stinky", "stormy", "stupid", "unfair", "unjust", "unwise", "wicked", "woeful", "abysmal", "adverse", "anxious", "beneath", "callous", "corrupt", "cutting", "disease", "enraged", "eroding", "ghastly", "grimace", "haggard", "harmful", "hideous", "hostile", "hurtful", "insipid", "jealous", "missing", "naughty", "noxious", "perturb", "revenge", "sobbing", "suspect", "unhappy", "unlucky", "vicious", "alarming", "collapse", "confused", "contrary", "criminal", "damaging", "decaying", "deformed", "deprived", "distress", "dreadful", "frighten", "gruesome", "horrible", "ignorant", "immature", "infernal", "menacing", "negative", "nonsense", "ruthless", "shocking", "sinister", "spiteful", "terrible", "untoward", "unwanted", "unwieldy", "appalling", "atrocious", "corrosive", "dastardly", "depressed", "dishonest", "frightful", "grotesque", "imperfect", "inelegant", "injurious", "insidious", "malicious", "misshapen", "monstrous", "offensive", "poisonous", "prejudice", "repellant", "reptilian", "repugnant", "repulsive", "revolting", "sickening", "stressful", "undermine", "unhealthy", "unsightly", "unwelcome", "worthless", "deplorable", "despicable", "disgusting", "disheveled", "horrendous", "impossible", "oppressive", "suspicious", "terrifying", "unpleasant", "villainous", "vindictive", "belligerent", "detrimental", "nondescript", "pessimistic", "substandard", "threatening", "unfavorable", "unwholesome", "dishonorable", "questionable", "contradictory", "misunderstood", "objectionable", "unsatisfactory" };
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

    public void OnEndDay()
    {

        foreach(GameObject w in activeWords.Values)
        {
            Destroy(w);
        }
        activeWords.Clear();
        this.enabled = false;
    }

    public void OnStartDay()
    {
        numWordsPiled = 0;
        this.enabled = true;
    }

    public void SubmitWord(string word)
    {
        GameObject wordTyped;
        if (activeWords.TryGetValue(word, out wordTyped))
        {
            activeWords.Remove(word);
            Destroy(wordTyped);
            correctWordEvent.Invoke(word.Length);
        }
        else
        {
            wrongWordEvent.Invoke();
        }
    }
}
