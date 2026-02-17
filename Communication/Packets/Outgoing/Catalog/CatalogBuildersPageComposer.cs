namespace WibboEmulator.Communication.Packets.Outgoing.Catalog;

using Core.Language;
using Games.Catalogs;
using Games.Catalogs.Builders;

public class CatalogBuildersPageComposer : ServerPacket
{
    public CatalogBuildersPageComposer(CatalogBuildersPage page, string cataMode, Language langue, int offerId = -1)
        : base(ServerPacketHeader.BUILDERS_PAGE)
    {
        this.WriteInteger(page.Id);
        this.WriteString(cataMode);
        this.WriteString(page.Template);

        this.WriteInteger(page.PageStrings1.Count);
        foreach (var s in page.PageStrings1)
        {
            this.WriteString(s);
        }

        if (page.GetPageStrings2ByLangue(langue).Count == 1 && (page.Template == "default_3x3" || page.Template == "default_3x3_color_grouping") && string.IsNullOrEmpty(page.GetPageStrings2ByLangue(langue)[0]))
        {
            this.WriteInteger(1);
            this.WriteString(string.Format(LanguageManager.TryGetValue("catalog.desc.default", langue), page.GetCaptionByLangue(langue)));
        }
        else
        {
            this.WriteInteger(page.GetPageStrings2ByLangue(langue).Count);
            foreach (var s in page.GetPageStrings2ByLangue(langue))
            {
                this.WriteString(s);
            }
        }

        if (!page.Template.Equals("frontpage") && !page.Template.Equals("club_buy"))
        {
            this.WriteInteger(page.Items.Count);
            foreach (var item in page.Items.Values)
            {
                CatalogItemUtility.GenerateBuilderOfferData(item, page.IsPremium, this);
            }
        }
        else
        {
            this.WriteInteger(0);
        }

        this.WriteInteger(offerId);
        this.WriteBoolean(false);

        this.WriteInteger(CatalogManager.Promotions.ToList().Count);//Count
        foreach (var promotion in CatalogManager.Promotions.ToList())
        {
            this.WriteInteger(promotion.Id);
            this.WriteString(promotion.GetTitleByLangue(langue));
            this.WriteString(promotion.Image);
            this.WriteInteger(promotion.Unknown);
            this.WriteString(promotion.PageLink);
            this.WriteInteger(promotion.ParentId);
        }
    }
}
