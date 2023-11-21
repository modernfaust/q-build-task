using Backend.Models;

public class DataService
{
    private readonly CsvParserService _csvParserService;
    private List<BillOfMaterialModel> _bomData;
    private List<PartModel> _partData;
    private List<PartDetailModel> _partDetailData;
    private List<Node> _indentedBomViewData;

    public DataService(CsvParserService csvParserService)
    {
        _csvParserService = csvParserService;
    }

    public class Node
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public List<Node> Children { get; set; } = new List<Node>();
    }

    public List<Node> BuildTree(List<BillOfMaterialModel> bomData, List<PartModel> partData, string parentName)
    {
        var nodes = new List<Node>();
        foreach (var item in bomData)
        {
            if (item.PARENT_NAME == parentName)
            {
                var node = new Node
                {
                    Name = item.COMPONENT_NAME,
                    Quantity = item.QUANTITY,
                    Children = BuildTree(bomData, partData, item.COMPONENT_NAME)
                };
                nodes.Add(node);
            }
        }
        return nodes;
    }

    public void LoadData(string filePath1, string filePath2)
    {
        _bomData = _csvParserService.ParseCsv<BillOfMaterialModel>(filePath1);
        _partData = _csvParserService.ParseCsv<PartModel>(filePath2);

        _partDetailData = (from part in _partData
                           join bom in _bomData on part.NAME equals bom.COMPONENT_NAME
                           select new PartDetailModel
                           {
                               NAME = part.NAME,
                               TYPE = part.TYPE,
                               ITEM = part.ITEM,
                               PART_NUMBER = part.PART_NUMBER,
                               TITLE = part.TITLE,
                               MATERIAL = part.MATERIAL,
                               PARENT_NAME = bom.PARENT_NAME,
                               QUANTITY = bom.QUANTITY
                           }).ToList();

        var rootNode = new Node
        {
            Name = "VALVE",
            Quantity = 1, // or whatever quantity you want for the root
            Children = BuildTree(_bomData, _partData, "VALVE")
        };

        _indentedBomViewData = new List<Node> { rootNode };
    }

    public List<BillOfMaterialModel> GetAllBillofMaterialItems()
    {
        return _bomData;
    }

    public List<PartModel> GetAllParts()
    {
        return _partData;
    }

    public List<Node> GetAllIndentedBomView()
    {
        return _indentedBomViewData;
    }

    public List<BillOfMaterialModel> GetDetailsByParent(string parent_name)
    {
        var matchingParts = new List<BillOfMaterialModel>();

        foreach (var bomItem in _bomData)
        {
            if (bomItem.PARENT_NAME == parent_name)
            {
                matchingParts.Add(bomItem);
            }
        }

        return matchingParts;
    }

    public List<PartDetailModel> GetComponentsByParent(string parent_name)
    {
        var matchingParts = new List<PartDetailModel>();

        foreach (var bomItem in _bomData)
        {
            if (bomItem.PARENT_NAME == parent_name)
            {
                foreach (var partItem in _partData)
                {
                    if (partItem.NAME == bomItem.COMPONENT_NAME)
                    {
                        var partDetailItem = new PartDetailModel
                        {
                            NAME = partItem.NAME,
                            TYPE = partItem.TYPE,
                            ITEM = partItem.ITEM,
                            PART_NUMBER = partItem.PART_NUMBER,
                            TITLE = partItem.TITLE,
                            MATERIAL = partItem.MATERIAL,
                            PARENT_NAME = bomItem.PARENT_NAME,
                            QUANTITY = bomItem.QUANTITY
                        };

                        matchingParts.Add(partDetailItem);
                    }
                }
            }
        }

        return matchingParts;
    }

    public List<PartDetailModel> GetDetailsByComponentName(string component_name)
    {
        var matchingParts = new List<PartDetailModel>();

        foreach (var partDetailItem in _partDetailData)
        {
            if (partDetailItem.NAME == component_name)
            {
                matchingParts.Add(partDetailItem);
            }
        }

        return matchingParts;
    }


    public List<PartDetailModel> GetComponentDetailsByParent(string parent_name, string component_name)
    {
        var matchingParts = new List<PartDetailModel>();

        foreach (var partDetailItem in _partDetailData)
        {
            if (partDetailItem.PARENT_NAME == parent_name && partDetailItem.NAME == component_name)
            {
                matchingParts.Add(partDetailItem);
            }
        }

        return matchingParts;
    }

}