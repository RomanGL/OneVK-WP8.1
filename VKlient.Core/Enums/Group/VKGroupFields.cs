namespace OneVK.Enums.Group
{
    /// <summary>
    /// Представляет перечисление дополнительных полей, которые 
    /// можно запросить методом ВКонтакте API <see cref="groups.getById"/>.
    /// </summary>
    public enum VKGroupFields
    {
        city,
        country,
        place,
        description,
        wiki_page,
        members_count,
        counters,
        start_date,
        finish_date,
        can_post,
        can_see_all_posts,
        activity,
        status,
        contacts,
        links,
        fixed_post,
        verified,
        site,
        ban_info
    }
}
