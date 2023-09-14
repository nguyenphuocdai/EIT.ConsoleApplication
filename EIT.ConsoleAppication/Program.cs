using EIT.ConsoleAppication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

internal class Program
{
    //private static string path = "D:\\RTDM_HCVN_2021.csv";
    private static string SeparateDefault = ";";

    private static string FolderChips = "offer-purpose/";
    private static string FolderDescriptions = "offer-detail/";

    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        //Console.WriteLine("1. CSV");
        //Console.WriteLine("2. JSON");
        //Console.Write("Enter mode: ");
        //string mode = Console.ReadLine();

        //Console.Write("Enter separate: ");
        //string separate = Console.ReadLine();

        //Console.Write("Enter path: ");
        //string path = Console.ReadLine();

        //Console.Write("Enter id template: ");
        //string idTemplate = Console.ReadLine();

        //string json = string.Empty;
        //if (mode == "1")
        //{
        //    if (string.IsNullOrWhiteSpace(separate))
        //    {
        //        separate = SeparateDefault;
        //    }

        //    json = ConvertCsvFileToJsonObject(path, char.Parse(separate));
        //}

        //if (mode == "2")
        //{
        //    json = LoadJson(path);
        //}
        //string str = "2,CCX offer,OFFERS,CreditCardPhysical,Thẻ tín dụng miễn phí,\"Rút tiền đến 100% hạn mức, hoàn tiền đến 10%\",,vn-hero-woman-holding-credit-card.jpg,offer-icon/card-image.svg,offer-image/model-holdingcounter.png";

        //string[] values = Regex.Split(str, @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))");

        //foreach (string value in values)
        //{
        //    Console.WriteLine(value.Trim('"'));
        //}

        //Console.ReadLine();
        //return;
        string json = ConvertCsvFileToJsonObject("D:\\RTDM_HCVN_2021.csv");

        List<RootData> rootData = Process(json);

        //if (string.IsNullOrWhiteSpace(IdTemplate) == false)
        //{
        //    var result = rootData.FirstOrDefault(i => i.id == IdTemplate);
        //    Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        //    Console.ReadLine();
        //    return;
        //}

        Console.WriteLine(JsonConvert.SerializeObject(rootData, Formatting.Indented));
        Console.ReadLine();
    }

    private static List<RootData> Process(string json)
    {
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
            chip1.imageUrl = $"{FolderChips}{jToken["chip1Image"]}";

            Chip chip2 = new Chip();
            chip2.title.vi = jToken["chip2"].ToString();
            chip2.imageUrl = $"{FolderChips}{jToken["chip2Image"]}"; 

            Chip chip3 = new Chip();
            chip3.title.vi = jToken["chip3"].ToString();
            chip3.imageUrl = $"{FolderChips}{jToken["chip3Image"]}";

            data.chips.Add(chip1);
            data.chips.Add(chip2);
            data.chips.Add(chip3);

            data.subtitleBanner.vi = jToken["subtitleBanner"].ToString();
            data.subtitleAmount.vi = jToken["subtitleAmount"].ToString();

            double.TryParse(jToken["LimitAmount"].ToString(), out double limitAmount);
            data.limitAmount = limitAmount == 0 ? null : limitAmount;


            data.actionStatement.vi = jToken["actionStatement"].ToString();

            Description description1 = new Description();
            description1.title.vi = jToken["descriptionTitle1"].ToString();
            description1.description.vi = jToken["description1"].ToString();
            description1.imageUrl = $"{FolderDescriptions}{jToken["iconDescription1"]}";

            Description description2 = new Description();
            description2.title.vi = jToken["descriptionTitle2"].ToString();
            description2.description.vi = jToken["description2"].ToString();
            description2.imageUrl = $"{FolderDescriptions}{jToken["iconDescription2"]}"; 

            Description description3 = new Description();
            description3.title.vi = jToken["descriptionTitle3"].ToString();
            description3.description.vi = jToken["description3"].ToString();
            description3.imageUrl = $"{FolderDescriptions}{jToken["iconDescription3"]}"; 

            Description description4 = new Description();
            description4.title.vi = jToken["descriptionTitle4"].ToString();
            description4.description.vi = jToken["description4"].ToString();
            description4.imageUrl = $"{FolderDescriptions}{jToken["iconDescription4"]}";

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

        return rootData;
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

    public static string ConvertCsvFileToJsonObject(string path, char separate = ',')
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return string.Empty;
        }

        var csv = new List<string[]>();
        var lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {
            string[] values = Regex.Split(line, @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))");

            var d = line.Split(separate);
            csv.Add(values);
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
                string value;
                try
                {
                    value = csv[i][j].Trim().Replace("\"", "");
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