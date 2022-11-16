using Recognizer;

internal class Program {
    private static void Main(string[] args) {
        var parser = new Recognizer.RecognizerClass(ExampleInput);
        if (parser.ParseValue()) {
            Console.WriteLine("Great success");
        } else {
            Console.WriteLine("Miserable Failure");
        }
    }

    private static string ExampleInput =
        "{\n" +
        "    \"version\": 17,\n" +
        "    \"bundles\": [\n" +
        "         {\"name\" : \"org.goral\" },\n" +
        "         {\"name\" : \"org.goral1\" },\n" +
        "         {\"name\" : \"org.goral2\" },\n" +
        "         {\"name\" : \"org.goral3\" " + "}\n" +
        "    ]\n" +
        "}";

}