namespace WibboEmulator.Database.Daos.Catalog;

using System.Data;
using Dapper;

internal sealed class CatalogBuildersPageDao
{
    internal static List<CatalogBuildersPageEntity> GetAll(IDbConnection dbClient) => dbClient.Query<CatalogBuildersPageEntity>(
        @"SELECT `catalog_bc_page`.id, `catalog_bc_page`.parent_id, `catalog_bc_page`.caption, `catalog_bc_page`.page_link, `catalog_bc_page`.enabled, `catalog_bc_page`.required_right, `catalog_bc_page`.icon_image,
        `catalog_bc_page`.page_layout, `catalog_bc_page`.page_strings_1, `catalog_bc_page`.page_strings_2, `catalog_page_langue`.caption_en, `catalog_page_langue`.caption_br,
        `catalog_page_langue`.page_strings_2_en, `catalog_page_langue`.page_strings_2_br, `catalog_bc_page`.is_premium
        FROM `catalog_bc_page`
        LEFT JOIN `catalog_page_langue` ON `catalog_bc_page`.id = `catalog_page_langue`.page_id
        ORDER BY order_num, caption"
    ).ToList();
}

public class CatalogBuildersPageEntity
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Caption { get; set; }
    public int IconImage { get; set; }
    public bool Enabled { get; set; }
    public string RequiredRight { get; set; }
    public int OrderNum { get; set; }
    public string PageLayout { get; set; }
    public string PageLink { get; set; }
    public string PageStrings1 { get; set; }
    public string PageStrings2 { get; set; }
    public bool IsPremium { get; set; }

    public string CaptionFr { get; set; }
    public string CaptionEn { get; set; }
    public string CaptionBr { get; set; }
    public string PageStrings2Fr { get; set; }
    public string PageStrings2En { get; set; }
    public string PageStrings2Br { get; set; }
}
