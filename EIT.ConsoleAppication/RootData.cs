namespace EIT.ConsoleAppication
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ActionStatement
    {
        public string vi { get; set; }
    }

    public class BannerId
    {
        public string vi { get; set; }
    }

    public class Chip
    {
        public Chip()
        {
            title = new Title();
        }

        public Title title { get; set; }
        public string imageUrl { get; set; }
    }

    public class Description
    {
        public Description()
        {
            title = new Title();
            description = new Description2();
        }

        public Title title { get; set; }
        public Description2 description { get; set; }
        public string imageUrl { get; set; }
    }

    public class Description2
    {
        public string vi { get; set; }
    }

    public class PersonalDataUsageAgreement
    {
        public string vi { get; set; }
    }

    public class PersonalDataUsageAgreementLink
    {
        public string vi { get; set; }
    }

    public class RootData
    {
        public RootData()
        {
            title = new Title();
            shortDescription = new ShortDescription();
            chips = new List<Chip>();
            subtitleBanner = new SubtitleBanner();
            subtitleAmount = new SubtitleAmount();
            actionStatement = new ActionStatement();
            descriptions = new List<Description>();
            bannerId = new BannerId();
            scoringDescription = new ScoringDescription();
            userInputDescription = new UserInputDescription();
            personalDataUsageAgreement = new PersonalDataUsageAgreement();
            personalDataUsageAgreementLink = new PersonalDataUsageAgreementLink();
        }

        public string id { get; set; }
        public string templateName { get; set; }
        public string type { get; set; }
        public string loanFlowType { get; set; }
        public Title title { get; set; } = new Title();
        public ShortDescription shortDescription { get; set; }
        public string offerIcon { get; set; }
        public string offerImage { get; set; }
        public List<Chip> chips { get; set; } 
        public SubtitleBanner subtitleBanner { get; set; } 
        public SubtitleAmount subtitleAmount { get; set; }
        public string limitAmount { get; set; }
        public ActionStatement actionStatement { get; set; }
        public List<Description> descriptions { get; set; }
        public BannerId bannerId { get; set; }
        public ScoringDescription scoringDescription { get; set; }
        public UserInputDescription userInputDescription { get; set; }
        public PersonalDataUsageAgreement personalDataUsageAgreement { get; set; }
        public PersonalDataUsageAgreementLink personalDataUsageAgreementLink { get; set; }
    }

    public class ScoringDescription
    {
        public string vi { get; set; }
    }

    public class ShortDescription
    {
        public string vi { get; set; }
    }

    public class SubtitleAmount
    {
        public string vi { get; set; }
    }

    public class SubtitleBanner
    {
        public string vi { get; set; }
    }

    public class Title
    {
        public string vi { get; set; }
    }

    public class UserInputDescription
    {
        public string vi { get; set; }
    }


}
