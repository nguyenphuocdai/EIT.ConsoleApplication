using EIT.ConsoleAppication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

internal class Program
{
    //private static string path = "D:\\RTDM_HCVN_2021.csv";
    private static void Main(string[] args)
    {
        //Console.WriteLine("Enter mode: ");
        //Console.WriteLine("1. CSV");
        //Console.WriteLine("2. JSON");
        //string mode = Console.ReadLine();

        //Console.WriteLine("Enter path: ");
        //string path = Console.ReadLine();

        //string json = string.Empty;
        //if (mode == "1")
        //{
        //    json = ConvertCsvFileToJsonObject(path);
        //}

        //if (mode == "2")
        //{
        //    json = LoadJson(path);
        //}

        string json = ConvertCsvFileToJsonObject("D:\\RTDM_HCVN_2021.csv");

        List<RootData> rootData = new List<RootData>();
        JArray array = JArray.Parse(json);

        for (int i = 0; i < array.Count; i++)
        {
            JToken jToken = array[i];
            RootData data = new RootData();
            data.id = jToken["ID_TEMPLATE"].ToString();
            data.templateName = jToken["TEMPLATE_NAME"].ToString();
            data.type = jToken["Type"].ToString();
            data.loanFlowType = jToken["loanFlowType"].ToString();

            data.title.vi = jToken["title"].ToString();
            data.shortDescription.vi = jToken["shortDescription"].ToString();
            data.offerIcon = jToken["offerIcon(old)"].ToString();
            data.offerImage = jToken["offerImage"].ToString();

            Chip chip1 = new Chip();
            chip1.title.vi = jToken["chip1"].ToString();
            chip1.imageUrl = jToken["chip1Image"].ToString();

            Chip chip2 = new Chip();
            chip2.title.vi = jToken["chip2"].ToString();
            chip2.imageUrl = jToken["chip2Image"].ToString();

            Chip chip3 = new Chip();
            chip3.title.vi = jToken["chip3"].ToString();
            chip3.imageUrl = jToken["chip3Image"].ToString();

            data.chips.Add(chip1);
            data.chips.Add(chip2);
            data.chips.Add(chip3);

            data.subtitleBanner.vi = jToken["subtitleBanner"].ToString();
            data.subtitleAmount.vi = jToken["subtitleAmount"].ToString();
            data.limitAmount = jToken["LimitAmount"].ToString();
            data.actionStatement.vi = jToken["actionStatement"].ToString();

            Description description1 = new Description();
            description1.title.vi = jToken["descriptionTitle1"].ToString();
            description1.description.vi = jToken["description1"].ToString();
            description1.imageUrl = jToken["iconDescription1"].ToString();

            Description description2 = new Description();
            description2.title.vi = jToken["descriptionTitle2"].ToString();
            description2.description.vi = jToken["description2"].ToString();
            description2.imageUrl = jToken["iconDescription2"].ToString();

            Description description3 = new Description();
            description3.title.vi = jToken["descriptionTitle3"].ToString();
            description3.description.vi = jToken["description3"].ToString();
            description3.imageUrl = jToken["iconDescription3"].ToString();

            Description description4 = new Description();
            description4.title.vi = jToken["descriptionTitle4"].ToString();
            description4.description.vi = jToken["description4"].ToString();
            description4.imageUrl = jToken["iconDescription4"].ToString();

            data.descriptions.Add(description1);
            data.descriptions.Add(description2);
            data.descriptions.Add(description3);
            data.descriptions.Add(description4);

            data.bannerId.vi = jToken["bannerId"].ToString();
            data.scoringDescription.vi = jToken["scoringDescription"].ToString();
            data.userInputDescription.vi = jToken["userInputDescription"].ToString();
            data.personalDataUsageAgreement.vi = jToken["personalDataUsageAgreement"].ToString();
            data.personalDataUsageAgreementLink.vi = jToken["personalDataUsageAgreementLink"].ToString();
            //data.personalDataUsageAgreementLink.vi = jToken["properties 36"].ToString();

            rootData.Add(data);
        }

        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine(JsonConvert.SerializeObject(rootData, Formatting.Indented));
        //Console.WriteLine(json);
        Console.ReadLine();
    }

    public static string LoadJson(string path)
    {
        try
        {
            using (StreamReader r = new StreamReader(path))
            {
                return r.ReadToEnd();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static string ConvertCsvFileToJsonObject(string path, char separate = ';')
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return string.Empty;
        }

        var csv = new List<string[]>();
        var lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {
            csv.Add(line.Split(separate));
        }

        var properties = lines[1].Split(separate);

        var listObjResult = new List<Dictionary<string, string>>();

        for (int i = 2; i < lines.Length; i++)
        {
            var line = lines[i];
            var id = line.Split(separate).FirstOrDefault();

            int.TryParse(id, out int templateId);
            if (templateId == 0)
            {
                continue;
            }

            var objResult = new Dictionary<string, string>();
            for (int j = 0; j < properties.Length; j++)
            {
                string value = string.Empty;
                try
                {
                    value = csv[i][j];
                }
                catch (Exception e)
                {
                    value = string.Empty;
                }

                objResult.Add(string.IsNullOrWhiteSpace(properties[j]) ? $"properties {j}" : properties[j], value);
            }

            listObjResult.Add(objResult);
        }

        return JsonConvert.SerializeObject(listObjResult, Formatting.Indented);
    }
}