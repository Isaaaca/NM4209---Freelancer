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
    private readonly Dictionary<int, int> sadListLetterCount = new Dictionary<int, int>
    {
        { 4, 32 },
        { 6, 106 },
        { 8, 163 },
        { 10, 205 }
    };

    private readonly string[] wordList_Romantic = { "fit", "fun", "hot", "cute", "rare", "foxy", "kind", "sexy", "thin", "warm", "wise", "brave", "curvy", "loved", "loyal", "frail", "funny", "happy", "juicy", "leggy", "sassy", "sharp", "smart", "sweet", "witty", "bright", "candid", "caring", "cheeky", "classy", "clever", "crafty", "cuddly", "daring", "dreamy", "lovely", "loving", "mature", "petite", "poetic", "pretty", "quirky", "feisty", "gentle", "gifted", "giving", "honest", "joyful", "sexual", "shrewd", "smiley", "spunky", "sultry", "unique", "upbeat", "keeper", "amazing", "angelic", "beloved", "blessed", "careful", "complex", "curious", "darling", "dynamic", "lovable", "magical", "musical", "naughty", "patient", "perfect", "playful", "radiant", "focused", "fragile", "genuine", "helpful", "refined", "sensual", "sincere", "special", "vibrant", "adorable", "alluring", "artistic", "charming", "cheerful", "creative", "cultured", "dazzling", "delicate", "luminous", "luscious", "magnetic", "narcotic", "positive", "precious", "engaging", "exciting", "fabulous", "fearless", "feminine", "friendly", "generous", "gorgeous", "graceful", "grounded", "handsome", "heavenly", "huggable", "hypnotic", "innocent", "kissable", "ladylike", "romantic", "selfless", "spirited", "talented", "trusting", "unafraid", "virtuous", "accepting", "agreeable", "attentive", "beautiful", "brilliant", "committed", "confident", "dedicated", "motivated", "nurturing", "ravishing", "energetic", "enigmatic", "exquisite", "forgiving", "glamorous", "hilarious", "inspiring", "intuitive", "seductive", "sensitive", "talkative", "unselfish", "vunerable", "wonderful", "attractive", "bewitching", "courageous", "delightful", "dependable", "mysterious", "passionate", "perceptive", "personable", "principled", "enchanting", "innovative", "remarkable", "respectful", "successful", "supportive", "thoughtful", "adventurous", "captivating", "considerate", "disciplined", "mesmerizing", "mischievous", "opinionated", "provocative", "hardworking", "independent", "intelligent", "kindhearted", "responsible", "sacrificing", "sentimental", "spontaneous", "sympathetic", "trustworthy", "affectionate", "breathtaking", "entertaining", "incomparable", "intellectual", "intoxicating", "compassionate", "extraordinary", "inspirational", "irreplaceable", "sophisticated", "understanding", "unpretentious" };
    private readonly Dictionary<int, int> romanticListLetterCount = new Dictionary<int, int>
    {
        { 4, 11 },
        { 6, 56 },
        { 8, 121 },
        { 10, 163 }
    };

    private readonly string[] wordList_Positive = { "fun", "hug", "joy", "now", "one", "wow", "yes", "calm", "cool", "cute", "easy", "fair", "fine", "free", "good", "grin", "idea", "kind", "nice", "okay", "open", "safe", "tops", "well", "zeal", "agree", "bliss", "brave", "bravo", "champ", "clean", "fresh", "funny", "great", "green", "happy", "ideal", "laugh", "light", "lucid", "lucky", "merit", "novel", "proud", "quick", "quiet", "ready", "right", "smile", "sunny", "super", "vital", "whole", "yummy", "action", "active", "admire", "bounty", "bubbly", "cheery", "choice", "divine", "famous", "genius", "giving", "hearty", "honest", "jovial", "lively", "lovely", "moving", "poised", "pretty", "reward", "robust", "secure", "seemly", "simple", "superb", "unreal", "upbeat", "valued", "worthy", "amazing", "angelic", "approve", "awesome", "beaming", "believe", "certain", "classic", "commend", "delight", "earnest", "elegant", "ethical", "fitting", "genuine", "glowing", "growing", "healing", "healthy", "honored", "imagine", "instant", "knowing", "learned", "natural", "perfect", "popular", "quality", "refined", "rejoice", "skilled", "soulful", "special", "success", "upright", "vibrant", "victory", "wealthy", "welcome", "willing", "zealous", "accepted", "adorable", "affluent", "aptitude", "champion", "charming", "composed", "constant", "creative", "dazzling", "ecstatic", "endorsed", "engaging", "esteemed", "exciting", "fabulous", "familiar", "fetching", "friendly", "generous", "gorgeous", "graceful", "handsome", "heavenly", "innovate", "luminous", "paradise", "pleasant", "polished", "positive", "powerful", "prepared", "progress", "reliable", "restored", "skillful", "spirited", "stirring", "stunning", "terrific", "thorough", "thriving", "tranquil", "trusting", "truthful", "vigorous", "virtuous", "wondrous", "acclaimed", "adventure", "agreeable", "appealing", "beautiful", "bountiful", "brilliant", "classical", "effective", "efficient", "energetic", "energized", "essential", "excellent", "exquisite", "fantastic", "favorable", "fortunate", "glamorous", "honorable", "intuitive", "inventive", "legendary", "marvelous", "masterful", "nurturing", "plentiful", "prominent", "protected", "respected", "rewarding", "sparkling", "spiritual", "thrilling", "vivacious", "wholesome", "wonderful", "absolutely", "accomplish", "attractive", "beneficial", "celebrated", "courageous", "delightful", "effortless", "enchanting", "harmonious", "impressive", "innovative", "meaningful", "miraculous", "motivating", "nutritious", "optimistic", "phenomenal", "principled", "productive", "reassuring", "refreshing", "remarkable", "resounding", "stupendous", "successful", "supporting", "surprising", "unwavering", "upstanding", "victorious", "achievement", "affirmative", "encouraging", "flourishing", "imaginative", "independent", "instinctive", "intelligent", "meritorious", "pleasurable", "effervescent", "electrifying", "enthusiastic", "intellectual", "satisfactory", "transforming", "distinguished", "instantaneous", "knowledgeable", "accomplishment", "congratulation", "transformative" };
    private readonly Dictionary<int, int> positiveListLetterCount = new Dictionary<int, int>
    {
        { 4, 25 },
        { 6, 83 },
        { 8, 172 },
        { 10, 240 }
    };

    // Start is called before the first frame update
    void Start()
    {
        activeWords = new SortedList<string, GameObject>();
        this.enabled = false;
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
        string[] wordList;
        Dictionary<int, int> letterCountIndex;

        int startIndex = 0, endIndex;
        
        if (Upgrades.hasUpgrade("positiveMindset"))
        {
            wordList = wordList_Positive;
            letterCountIndex = positiveListLetterCount;
        }
        else if (Upgrades.hasUpgrade("romanticMindset"))
        {
            wordList = wordList_Romantic;
            letterCountIndex = romanticListLetterCount;
        }
        else
        {
            wordList = wordList_Sad;
            letterCountIndex = sadListLetterCount;
        }
        endIndex = wordList.Length;
        if (Upgrades.hasUpgrade("shorterWords"))
        {
            if (Upgrades.hasUpgrade("onlyShortWords"))
            {
                endIndex = letterCountIndex[6];
            }
            else
            {
                endIndex = letterCountIndex[8];
            }
                startIndex = 0;
        }
        else if (Upgrades.hasUpgrade("longerWords"))
        {
            if (Upgrades.hasUpgrade("onlyLongWords"))
            {
                startIndex = letterCountIndex[8];
            }
            else
            {
                startIndex = letterCountIndex[6];
            }
            endIndex = wordList.Length;
        }
        return wordList[Random.Range(startIndex, endIndex)];
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
