











using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitterAPI.Models
{

    public partial class Target
    {
        

		[JsonProperty("id_str")]
        public string Id_str { get; set; }

		[JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("followed_by")]
        public bool Followed_by { get; set; }

		[JsonProperty("screen_name")]
        public string Screen_name { get; set; }

		[JsonProperty("following")]
        public bool Following { get; set; }

    } // end class


    public partial class Source
    {
        

		[JsonProperty("can_dm")]
        public bool Can_dm { get; set; }

		[JsonProperty("blocking")]
        public bool Blocking { get; set; }

		[JsonProperty("id_str")]
        public string Id_str { get; set; }

		[JsonProperty("all_replies")]
        public bool All_replies { get; set; }

		[JsonProperty("want_retweets")]
        public bool Want_retweets { get; set; }

		[JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("marked_spam")]
        public bool Marked_spam { get; set; }

		[JsonProperty("followed_by")]
        public bool Followed_by { get; set; }

		[JsonProperty("notifications_enabled")]
        public bool Notifications_enabled { get; set; }

		[JsonProperty("screen_name")]
        public string Screen_name { get; set; }

		[JsonProperty("following")]
        public bool Following { get; set; }

    } // end class


    public partial class Relationship
    {
        

		[JsonProperty("target")]
        public Target Target { get; set; }

		[JsonProperty("source")]
        public Source Source { get; set; }

    } // end class


    public partial class UpdateMediaTypeExtensionPostOKResponseContent
    {
        

		[JsonProperty("relationship")]
        public Relationship Relationship { get; set; }

    } // end class


    public partial class ShowMediaTypeExtensionGetOKResponseContent
    {
        

		[JsonProperty("relationship")]
        public Relationship Relationship { get; set; }

    } // end class


    public partial class UpdateProfileBackgroundImageMediaTypeExtensionPostOKResponseContent
    {
        

		[JsonProperty("contributors_enabled")]
        public bool Contributors_enabled { get; set; }

		[JsonProperty("created_at")]
        public string Created_at { get; set; }

		[JsonProperty("default_profile")]
        public bool Default_profile { get; set; }

		[JsonProperty("default_profile_image")]
        public bool Default_profile_image { get; set; }

		[JsonProperty("description")]
        public string Description { get; set; }

		[JsonProperty("favourites_count")]
        public int Favourites_count { get; set; }

		[JsonProperty("follow_request_sent")]
        public bool Follow_request_sent { get; set; }

		[JsonProperty("followers_count")]
        public int Followers_count { get; set; }

		[JsonProperty("following")]
        public bool Following { get; set; }

		[JsonProperty("friends_count")]
        public int Friends_count { get; set; }

		[JsonProperty("geo_enabled")]
        public bool Geo_enabled { get; set; }

		[JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("id_str")]
        public string Id_str { get; set; }

		[JsonProperty("is_translator")]
        public bool Is_translator { get; set; }

		[JsonProperty("lang")]
        public string Lang { get; set; }

		[JsonProperty("listed_count")]
        public int Listed_count { get; set; }

		[JsonProperty("location")]
        public string Location { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("notifications")]
        public bool Notifications { get; set; }

		[JsonProperty("profile_background_color")]
        public string Profile_background_color { get; set; }

		[JsonProperty("profile_background_image_url")]
        public string Profile_background_image_url { get; set; }

		[JsonProperty("profile_background_image_url_https")]
        public string Profile_background_image_url_https { get; set; }

		[JsonProperty("profile_background_tile")]
        public bool Profile_background_tile { get; set; }

		[JsonProperty("profile_image_url")]
        public string Profile_image_url { get; set; }

		[JsonProperty("profile_image_url_https")]
        public string Profile_image_url_https { get; set; }

		[JsonProperty("profile_link_color")]
        public string Profile_link_color { get; set; }

		[JsonProperty("profile_sidebar_border_color")]
        public string Profile_sidebar_border_color { get; set; }

		[JsonProperty("profile_sidebar_fill_color")]
        public string Profile_sidebar_fill_color { get; set; }

		[JsonProperty("profile_text_color")]
        public string Profile_text_color { get; set; }

		[JsonProperty("profile_use_background_image")]
        public bool Profile_use_background_image { get; set; }

		[JsonProperty("protected")]
        public bool Protected { get; set; }

		[JsonProperty("screen_name")]
        public string Screen_name { get; set; }

		[JsonProperty("show_all_inline_media")]
        public bool Show_all_inline_media { get; set; }

		[JsonProperty("statuses_count")]
        public int Statuses_count { get; set; }

		[JsonProperty("time_zone")]
        public string Time_zone { get; set; }

		[JsonProperty("url")]
        public string Url { get; set; }

		[JsonProperty("utc_offset")]
        public int Utc_offset { get; set; }

		[JsonProperty("verified")]
        public bool Verified { get; set; }

    } // end class


    public partial class User
    {
        

		[JsonProperty("geo_enabled")]
        public bool Geo_enabled { get; set; }

		[JsonProperty("profile_background_image_url_https")]
        public string Profile_background_image_url_https { get; set; }

		[JsonProperty("profile_background_color")]
        public string Profile_background_color { get; set; }

		[JsonProperty("protected")]
        public bool Protected { get; set; }

		[JsonProperty("default_profile")]
        public bool Default_profile { get; set; }

		[JsonProperty("listed_count")]
        public int Listed_count { get; set; }

		[JsonProperty("profile_background_tile")]
        public bool Profile_background_tile { get; set; }

		[JsonProperty("created_at")]
        public string Created_at { get; set; }

		[JsonProperty("friends_count")]
        public int Friends_count { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("profile_sidebar_fill_color")]
        public string Profile_sidebar_fill_color { get; set; }

		[JsonProperty("notifications")]
        public bool Notifications { get; set; }

		[JsonProperty("utc_offset")]
        public int Utc_offset { get; set; }

		[JsonProperty("profile_image_url_https")]
        public string Profile_image_url_https { get; set; }

		[JsonProperty("description")]
        public string Description { get; set; }

		[JsonProperty("display_url")]
        public string Display_url { get; set; }

		[JsonProperty("following")]
        public bool Following { get; set; }

		[JsonProperty("verified")]
        public bool Verified { get; set; }

		[JsonProperty("favourites_count")]
        public int Favourites_count { get; set; }

		[JsonProperty("profile_sidebar_border_color")]
        public string Profile_sidebar_border_color { get; set; }

		[JsonProperty("followers_count")]
        public int Followers_count { get; set; }

		[JsonProperty("profile_image_url")]
        public string Profile_image_url { get; set; }

		[JsonProperty("default_profile_image")]
        public bool Default_profile_image { get; set; }

		[JsonProperty("contributors_enabled")]
        public bool Contributors_enabled { get; set; }

		[JsonProperty("deactivated_bit")]
        public bool Deactivated_bit { get; set; }

		[JsonProperty("statuses_count")]
        public int Statuses_count { get; set; }

		[JsonProperty("profile_use_background_image")]
        public bool Profile_use_background_image { get; set; }

		[JsonProperty("location")]
        public string Location { get; set; }

		[JsonProperty("id_str")]
        public string Id_str { get; set; }

		[JsonProperty("show_all_inline_media")]
        public bool Show_all_inline_media { get; set; }

		[JsonProperty("profile_text_color")]
        public string Profile_text_color { get; set; }

		[JsonProperty("screen_name")]
        public string Screen_name { get; set; }

		[JsonProperty("follow_request_sent")]
        public bool Follow_request_sent { get; set; }

		[JsonProperty("profile_background_image_url")]
        public string Profile_background_image_url { get; set; }

		[JsonProperty("url")]
        public string Url { get; set; }

		[JsonProperty("expanded_url")]
        public string Expanded_url { get; set; }

		[JsonProperty("is_translator")]
        public bool Is_translator { get; set; }

		[JsonProperty("time_zone")]
        public string Time_zone { get; set; }

		[JsonProperty("profile_link_color")]
        public string Profile_link_color { get; set; }

		[JsonProperty("id")]
        public int Id { get; set; }

    } // end class


    public partial class DestroyMediaTypeExtensionPostOKResponseContent
    {
        

		[JsonProperty("created_at")]
        public string Created_at { get; set; }

		[JsonProperty("slug")]
        public string Slug { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("full_name")]
        public string Full_name { get; set; }

		[JsonProperty("description")]
        public string Description { get; set; }

		[JsonProperty("mode")]
        public string Mode { get; set; }

		[JsonProperty("following")]
        public bool Following { get; set; }

		[JsonProperty("user")]
        public User User { get; set; }

		[JsonProperty("member_count")]
        public int Member_count { get; set; }

		[JsonProperty("id_str")]
        public string Id_str { get; set; }

		[JsonProperty("subscriber_count")]
        public int Subscriber_count { get; set; }

		[JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("uri")]
        public string Uri { get; set; }

    } // end class


    public partial class CreateMediaTypeExtensionPostOKResponseContent
    {
        

		[JsonProperty("created_at")]
        public string Created_at { get; set; }

		[JsonProperty("slug")]
        public string Slug { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("full_name")]
        public string Full_name { get; set; }

		[JsonProperty("description")]
        public string Description { get; set; }

		[JsonProperty("mode")]
        public string Mode { get; set; }

		[JsonProperty("following")]
        public bool Following { get; set; }

		[JsonProperty("user")]
        public User User { get; set; }

		[JsonProperty("member_count")]
        public int Member_count { get; set; }

		[JsonProperty("id_str")]
        public string Id_str { get; set; }

		[JsonProperty("subscriber_count")]
        public int Subscriber_count { get; set; }

		[JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("uri")]
        public string Uri { get; set; }

    } // end class


    public partial class A
    {
        

		[JsonProperty("created_at")]
        public string Created_at { get; set; }

		[JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("id_str")]
        public string Id_str { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("position")]
        public string Position { get; set; }

		[JsonProperty("query")]
        public string Query { get; set; }

    } // end class


    public partial class ListMediaTypeExtensionGetOKResponseContent
    {
        

        public A A { get; set; }

    } // end class


    public partial class ShowIdGetOKResponseContent
    {
        

		[JsonProperty("created_at")]
        public string Created_at { get; set; }

		[JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("id_str")]
        public string Id_str { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("position")]
        public string Position { get; set; }

		[JsonProperty("query")]
        public string Query { get; set; }

    } // end class


    public partial class DestroyIdPostOKResponseContent
    {
        

		[JsonProperty("created_at")]
        public string Created_at { get; set; }

		[JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("id_str")]
        public string Id_str { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("position")]
        public string Position { get; set; }

		[JsonProperty("query")]
        public string Query { get; set; }

    } // end class


    public partial class Attributes
    {
        

		[JsonProperty("162834:id")]
        public string P162834id { get; set; }

    } // end class


    public partial class BoundingBox
    {
        

		[JsonProperty("type")]
        public string Type { get; set; }

    } // end class


    public partial class ContainedWithin
    {
        

		[JsonProperty("bounding_box")]
        public BoundingBox Bounding_box { get; set; }

		[JsonProperty("country_code")]
        public string Country_code { get; set; }

		[JsonProperty("country")]
        public string Country { get; set; }

		[JsonProperty("full_name")]
        public string Full_name { get; set; }

		[JsonProperty("id")]
        public string Id { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("place_type")]
        public string Place_type { get; set; }

		[JsonProperty("url")]
        public string Url { get; set; }

    } // end class


    public partial class Geometry
    {
        

		[JsonProperty("type")]
        public string Type { get; set; }

    } // end class


    public partial class IdPlaceIdMediaTypeExtensionGetOKResponseContent
    {
        

		[JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

		[JsonProperty("bounding_box")]
        public BoundingBox Bounding_box { get; set; }

		[JsonProperty("contained_within")]
        public ContainedWithin[] Contained_within { get; set; }

		[JsonProperty("country_code")]
        public string Country_code { get; set; }

		[JsonProperty("country")]
        public string Country { get; set; }

		[JsonProperty("full_name")]
        public string Full_name { get; set; }

		[JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

		[JsonProperty("id")]
        public string Id { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("place_type")]
        public string Place_type { get; set; }

		[JsonProperty("polylines")]
        public string[] Polylines { get; set; }

		[JsonProperty("url")]
        public string Url { get; set; }

    } // end class


    public partial class ReverseGeocodeMediaTypeExtensionGetOKResponseContent
    {
        

		[JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

		[JsonProperty("bounding_box")]
        public BoundingBox Bounding_box { get; set; }

		[JsonProperty("contained_within")]
        public ContainedWithin[] Contained_within { get; set; }

		[JsonProperty("country_code")]
        public string Country_code { get; set; }

		[JsonProperty("country")]
        public string Country { get; set; }

		[JsonProperty("full_name")]
        public string Full_name { get; set; }

		[JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

		[JsonProperty("id")]
        public string Id { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("place_type")]
        public string Place_type { get; set; }

		[JsonProperty("polylines")]
        public string[] Polylines { get; set; }

		[JsonProperty("url")]
        public string Url { get; set; }

    } // end class


    public partial class SearchMediaTypeExtensionGetOKResponseContent
    {
        

		[JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

		[JsonProperty("bounding_box")]
        public BoundingBox Bounding_box { get; set; }

		[JsonProperty("contained_within")]
        public ContainedWithin[] Contained_within { get; set; }

		[JsonProperty("country_code")]
        public string Country_code { get; set; }

		[JsonProperty("country")]
        public string Country { get; set; }

		[JsonProperty("full_name")]
        public string Full_name { get; set; }

		[JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

		[JsonProperty("id")]
        public string Id { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("place_type")]
        public string Place_type { get; set; }

		[JsonProperty("polylines")]
        public string[] Polylines { get; set; }

		[JsonProperty("url")]
        public string Url { get; set; }

    } // end class


    public partial class SimilarPlacesMediaTypeExtensionGetOKResponseContent
    {
        

		[JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

		[JsonProperty("bounding_box")]
        public BoundingBox Bounding_box { get; set; }

		[JsonProperty("contained_within")]
        public ContainedWithin[] Contained_within { get; set; }

		[JsonProperty("country_code")]
        public string Country_code { get; set; }

		[JsonProperty("country")]
        public string Country { get; set; }

		[JsonProperty("full_name")]
        public string Full_name { get; set; }

		[JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

		[JsonProperty("id")]
        public string Id { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("place_type")]
        public string Place_type { get; set; }

		[JsonProperty("polylines")]
        public string[] Polylines { get; set; }

		[JsonProperty("url")]
        public string Url { get; set; }

    } // end class


    public partial class PlaceMediaTypeExtensionPostOKResponseContent
    {
        

		[JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

		[JsonProperty("bounding_box")]
        public BoundingBox Bounding_box { get; set; }

		[JsonProperty("contained_within")]
        public ContainedWithin[] Contained_within { get; set; }

		[JsonProperty("country_code")]
        public string Country_code { get; set; }

		[JsonProperty("country")]
        public string Country { get; set; }

		[JsonProperty("full_name")]
        public string Full_name { get; set; }

		[JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

		[JsonProperty("id")]
        public string Id { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("place_type")]
        public string Place_type { get; set; }

		[JsonProperty("polylines")]
        public string[] Polylines { get; set; }

		[JsonProperty("url")]
        public string Url { get; set; }

    } // end class


    public partial class PlaceMediaTypeExtensionGetOKResponseContent
    {
        

        public A A { get; set; }

    } // end class


    public partial class AvailableMediaTypeExtensionGetOKResponseContent
    {
        

        public A A { get; set; }

    } // end class


    public partial class ClosestMediaTypeExtensionGetOKResponseContent
    {
        

        public A A { get; set; }

    } // end class


    public partial class PrivacyMediaTypeExtensionGetOKResponseContent
    {
        

		[JsonProperty("privacy")]
        public string Privacy { get; set; }

    } // end class


    public partial class TosMediaTypeExtensionGetOKResponseContent
    {
        

		[JsonProperty("tos")]
        public string Tos { get; set; }

    } // end class


    public partial class RateLimitContext
    {
        

		[JsonProperty("access_token")]
        public string Access_token { get; set; }

    } // end class


    public partial class HelpConfiguration
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class HelpLanguages
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class HelpPrivacy
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class HelpTos
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class Help
    {
        

		[JsonProperty("/help/configuration")]
        public HelpConfiguration Helpconfiguration { get; set; }

		[JsonProperty("/help/languages")]
        public HelpLanguages Helplanguages { get; set; }

		[JsonProperty("/help/privacy")]
        public HelpPrivacy Helpprivacy { get; set; }

		[JsonProperty("/help/tos")]
        public HelpTos Helptos { get; set; }

    } // end class


    public partial class SearchTweets
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class Search
    {
        

		[JsonProperty("/search/tweets")]
        public SearchTweets Searchtweets { get; set; }

    } // end class


    public partial class StatusesHomeTimeline
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class StatusesMentionsTimeline
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class StatusesOembed
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class StatusesRetweetsId
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class StatusesShowId
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class StatusesUserTimeline
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class Statuses
    {
        

		[JsonProperty("/statuses/home_timeline")]
        public StatusesHomeTimeline Statuseshome_timeline { get; set; }

		[JsonProperty("/statuses/mentions_timeline")]
        public StatusesMentionsTimeline Statusesmentions_timeline { get; set; }

		[JsonProperty("/statuses/oembed")]
        public StatusesOembed Statusesoembed { get; set; }

		[JsonProperty("/statuses/retweets/:id")]
        public StatusesRetweetsId Statusesretweetsid { get; set; }

		[JsonProperty("/statuses/show/:id")]
        public StatusesShowId Statusesshowid { get; set; }

		[JsonProperty("/statuses/user_timeline")]
        public StatusesUserTimeline Statusesuser_timeline { get; set; }

    } // end class


    public partial class UsersContributees
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class UsersContributors
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class UsersLookup
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class UsersSearch
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class UsersShow
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class UsersSuggestionsSlugMembers
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class UsersSuggestionsSlug
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class UsersSuggestions
    {
        

		[JsonProperty("limit")]
        public decimal Limit { get; set; }

		[JsonProperty("remaining")]
        public decimal Remaining { get; set; }

		[JsonProperty("reset")]
        public decimal Reset { get; set; }

    } // end class


    public partial class Users
    {
        

		[JsonProperty("/users/contributees")]
        public UsersContributees Userscontributees { get; set; }

		[JsonProperty("/users/contributors")]
        public UsersContributors Userscontributors { get; set; }

		[JsonProperty("/users/lookup")]
        public UsersLookup Userslookup { get; set; }

		[JsonProperty("/users/search")]
        public UsersSearch Userssearch { get; set; }

		[JsonProperty("/users/show")]
        public UsersShow Usersshow { get; set; }

		[JsonProperty("/users/suggestions/:slug/members")]
        public UsersSuggestionsSlugMembers Userssuggestionsslugmembers { get; set; }

		[JsonProperty("/users/suggestions/:slug")]
        public UsersSuggestionsSlug Userssuggestionsslug { get; set; }

		[JsonProperty("/users/suggestions")]
        public UsersSuggestions Userssuggestions { get; set; }

    } // end class


    public partial class Resources
    {
        

		[JsonProperty("help")]
        public Help Help { get; set; }

		[JsonProperty("search")]
        public Search Search { get; set; }

		[JsonProperty("statuses")]
        public Statuses Statuses { get; set; }

		[JsonProperty("users")]
        public Users Users { get; set; }

    } // end class


    public partial class ApplicationRateLimitStatusMediaTypeExtensionGetOKResponseContent
    {
        

		[JsonProperty("rate_limit_context")]
        public RateLimitContext Rate_limit_context { get; set; }

		[JsonProperty("resources")]
        public Resources Resources { get; set; }

    } // end class


    // Unable to parse the following Schemas. Please note that JSON Schema version 4 is not supported

    // /mentions_timeline{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /user_timeline{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /home_timeline{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /retweets_of_me{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /retweets/{id}{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /show/{id}{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /destroy/{id}{mediaTypeExtension}-postOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /update{mediaTypeExtension}-postOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /retweet/{id}{mediaTypeExtension}-postOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /update_with_media{mediaTypeExtension}-postOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /retweeters/ids{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.ids', line 8, position 17.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.ids', line 8, position 17.

    // /search/tweets{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /direct_messages{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /sent{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /show{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /destroy{mediaTypeExtension}-postOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /new{mediaTypeExtension}-postOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /no_retweets/ids{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 11.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 11.

    // /lookup{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /incoming{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.ids', line 8, position 17.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.ids', line 8, position 17.

    // /outgoing{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.ids', line 8, position 17.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.ids', line 8, position 17.

    // /create{mediaTypeExtension}-postOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /ids{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.ids', line 8, position 17.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.ids', line 8, position 17.

    // /list{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.users', line 14, position 19.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.users', line 14, position 19.

    // /settings{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.trend_location', line 54, position 28.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.trend_location', line 54, position 28.

    // /settings{mediaTypeExtension}-postOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.trend_location', line 54, position 28.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.trend_location', line 54, position 28.

    // /verify_credentials{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.status.properties.coordinates.properties.coordinates', line 111, position 41.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.status.properties.coordinates.properties.coordinates', line 111, position 41.

    // /update_profile_colors{mediaTypeExtension}-postOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.entities.properties.url.properties.urls', line 28, position 28.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.entities.properties.url.properties.urls', line 28, position 28.

    // /update_profile_image{mediaTypeExtension}-postOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got String. Path 'properties.screen_name', line 98, position 37.. v4 parser message: Expected object while parsing schema object, got String. Path 'properties.screen_name', line 98, position 37.

    // /search{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /contributees{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /contributors{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /suggestions{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /{slug}{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.users', line 14, position 19.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.users', line 14, position 19.

    // /members{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /report_spam{mediaTypeExtension}-postOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /statuses{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /memberships{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /subscribers{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /create-postOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /show-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /subscriptions{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /ownerships{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

    // /configuration{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.non_username_paths', line 11, position 32.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.non_username_paths', line 11, position 32.

    // /languages{mediaTypeExtension}-getOKResponseContent - Could not parse JSON Schema. v3 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.. v4 parser message: Expected object while parsing schema object, got Array. Path 'properties.', line 5, position 14.

} // end Objects namespace