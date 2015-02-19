
using System;
using System.Collections.Generic;

namespace TwitterAPI.Objects
{
    public partial class Target
    {
        
        public string Id_str { get; set; }
        public int Id { get; set; }
        public bool Followed_by { get; set; }
        public string Screen_name { get; set; }
        public bool Following { get; set; }
    } // end class

    public partial class Source
    {
        
        public bool Can_dm { get; set; }
        public bool Blocking { get; set; }
        public string Id_str { get; set; }
        public bool All_replies { get; set; }
        public bool Want_retweets { get; set; }
        public int Id { get; set; }
        public bool Marked_spam { get; set; }
        public bool Followed_by { get; set; }
        public bool Notifications_enabled { get; set; }
        public string Screen_name { get; set; }
        public bool Following { get; set; }
    } // end class

    public partial class Relationship
    {
        
        public Target Target { get; set; }
        public Source Source { get; set; }
    } // end class

    public partial class UpdateMediaTypeExtensionPostOKResponseContent
    {
        
        public Relationship Relationship { get; set; }
    } // end class

    public partial class ShowMediaTypeExtensionGetOKResponseContent
    {
        
        public Relationship Relationship { get; set; }
    } // end class

    public partial class UpdateProfileBackgroundImageMediaTypeExtensionPostOKResponseContent
    {
        
        public bool Contributors_enabled { get; set; }
        public string Created_at { get; set; }
        public bool Default_profile { get; set; }
        public bool Default_profile_image { get; set; }
        public string Description { get; set; }
        public int Favourites_count { get; set; }
        public bool Follow_request_sent { get; set; }
        public int Followers_count { get; set; }
        public bool Following { get; set; }
        public int Friends_count { get; set; }
        public bool Geo_enabled { get; set; }
        public int Id { get; set; }
        public string Id_str { get; set; }
        public bool Is_translator { get; set; }
        public string Lang { get; set; }
        public int Listed_count { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public bool Notifications { get; set; }
        public string Profile_background_color { get; set; }
        public string Profile_background_image_url { get; set; }
        public string Profile_background_image_url_https { get; set; }
        public bool Profile_background_tile { get; set; }
        public string Profile_image_url { get; set; }
        public string Profile_image_url_https { get; set; }
        public string Profile_link_color { get; set; }
        public string Profile_sidebar_border_color { get; set; }
        public string Profile_sidebar_fill_color { get; set; }
        public string Profile_text_color { get; set; }
        public bool Profile_use_background_image { get; set; }
        public bool Protected { get; set; }
        public string Screen_name { get; set; }
        public bool Show_all_inline_media { get; set; }
        public int Statuses_count { get; set; }
        public string Time_zone { get; set; }
        public string Url { get; set; }
        public int Utc_offset { get; set; }
        public bool Verified { get; set; }
    } // end class

    public partial class User
    {
        
        public bool Geo_enabled { get; set; }
        public string Profile_background_image_url_https { get; set; }
        public string Profile_background_color { get; set; }
        public bool Protected { get; set; }
        public bool Default_profile { get; set; }
        public int Listed_count { get; set; }
        public bool Profile_background_tile { get; set; }
        public string Created_at { get; set; }
        public int Friends_count { get; set; }
        public string Name { get; set; }
        public string Profile_sidebar_fill_color { get; set; }
        public bool Notifications { get; set; }
        public int Utc_offset { get; set; }
        public string Profile_image_url_https { get; set; }
        public string Description { get; set; }
        public string Display_url { get; set; }
        public bool Following { get; set; }
        public bool Verified { get; set; }
        public int Favourites_count { get; set; }
        public string Profile_sidebar_border_color { get; set; }
        public int Followers_count { get; set; }
        public string Profile_image_url { get; set; }
        public bool Default_profile_image { get; set; }
        public bool Contributors_enabled { get; set; }
        public bool Deactivated_bit { get; set; }
        public int Statuses_count { get; set; }
        public bool Profile_use_background_image { get; set; }
        public string Location { get; set; }
        public string Id_str { get; set; }
        public bool Show_all_inline_media { get; set; }
        public string Profile_text_color { get; set; }
        public string Screen_name { get; set; }
        public bool Follow_request_sent { get; set; }
        public string Profile_background_image_url { get; set; }
        public string Url { get; set; }
        public string Expanded_url { get; set; }
        public bool Is_translator { get; set; }
        public string Time_zone { get; set; }
        public string Profile_link_color { get; set; }
        public int Id { get; set; }
    } // end class

    public partial class DestroyMediaTypeExtensionPostOKResponseContent
    {
        
        public string Created_at { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Full_name { get; set; }
        public string Description { get; set; }
        public string Mode { get; set; }
        public bool Following { get; set; }
        public User User { get; set; }
        public int Member_count { get; set; }
        public string Id_str { get; set; }
        public int Subscriber_count { get; set; }
        public int Id { get; set; }
        public string Uri { get; set; }
    } // end class

    public partial class CreateMediaTypeExtensionPostOKResponseContent
    {
        
        public string Created_at { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Full_name { get; set; }
        public string Description { get; set; }
        public string Mode { get; set; }
        public bool Following { get; set; }
        public User User { get; set; }
        public int Member_count { get; set; }
        public string Id_str { get; set; }
        public int Subscriber_count { get; set; }
        public int Id { get; set; }
        public string Uri { get; set; }
    } // end class

    public partial class A
    {
        
        public string Created_at { get; set; }
        public int Id { get; set; }
        public string Id_str { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Query { get; set; }
    } // end class

    public partial class ListMediaTypeExtensionGetOKResponseContent
    {
        
        public A A { get; set; }
    } // end class

    public partial class ShowIdGetOKResponseContent
    {
        
        public string Created_at { get; set; }
        public int Id { get; set; }
        public string Id_str { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Query { get; set; }
    } // end class

    public partial class DestroyIdPostOKResponseContent
    {
        
        public string Created_at { get; set; }
        public int Id { get; set; }
        public string Id_str { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Query { get; set; }
    } // end class

    public partial class Attributes
    {
        
        public string P162834id { get; set; }
    } // end class

    public partial class BoundingBox
    {
        
        public string Type { get; set; }
    } // end class

    public partial class ContainedWithin
    {
        
        public BoundingBox Bounding_box { get; set; }
        public string Country_code { get; set; }
        public string Country { get; set; }
        public string Full_name { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Place_type { get; set; }
        public string Url { get; set; }
    } // end class

    public partial class Geometry
    {
        
        public string Type { get; set; }
    } // end class

    public partial class IdPlaceIdMediaTypeExtensionGetOKResponseContent
    {
        
        public Attributes Attributes { get; set; }
        public BoundingBox Bounding_box { get; set; }
        public ContainedWithin[] Contained_within { get; set; }
        public string Country_code { get; set; }
        public string Country { get; set; }
        public string Full_name { get; set; }
        public Geometry Geometry { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Place_type { get; set; }
        public string[] Polylines { get; set; }
        public string Url { get; set; }
    } // end class

    public partial class ReverseGeocodeMediaTypeExtensionGetOKResponseContent
    {
        
        public Attributes Attributes { get; set; }
        public BoundingBox Bounding_box { get; set; }
        public ContainedWithin[] Contained_within { get; set; }
        public string Country_code { get; set; }
        public string Country { get; set; }
        public string Full_name { get; set; }
        public Geometry Geometry { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Place_type { get; set; }
        public string[] Polylines { get; set; }
        public string Url { get; set; }
    } // end class

    public partial class SearchMediaTypeExtensionGetOKResponseContent
    {
        
        public Attributes Attributes { get; set; }
        public BoundingBox Bounding_box { get; set; }
        public ContainedWithin[] Contained_within { get; set; }
        public string Country_code { get; set; }
        public string Country { get; set; }
        public string Full_name { get; set; }
        public Geometry Geometry { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Place_type { get; set; }
        public string[] Polylines { get; set; }
        public string Url { get; set; }
    } // end class

    public partial class SimilarPlacesMediaTypeExtensionGetOKResponseContent
    {
        
        public Attributes Attributes { get; set; }
        public BoundingBox Bounding_box { get; set; }
        public ContainedWithin[] Contained_within { get; set; }
        public string Country_code { get; set; }
        public string Country { get; set; }
        public string Full_name { get; set; }
        public Geometry Geometry { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Place_type { get; set; }
        public string[] Polylines { get; set; }
        public string Url { get; set; }
    } // end class

    public partial class PlaceMediaTypeExtensionPostOKResponseContent
    {
        
        public Attributes Attributes { get; set; }
        public BoundingBox Bounding_box { get; set; }
        public ContainedWithin[] Contained_within { get; set; }
        public string Country_code { get; set; }
        public string Country { get; set; }
        public string Full_name { get; set; }
        public Geometry Geometry { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Place_type { get; set; }
        public string[] Polylines { get; set; }
        public string Url { get; set; }
    } // end class

    public partial class Locations
    {
        
        public string Name { get; set; }
        public decimal Woeid { get; set; }
    } // end class

    public partial class Trends
    {
        
        public string Name { get; set; }
        public string Query { get; set; }
        public string Url { get; set; }
    } // end class

    public partial class PlaceMediaTypeExtensionGetOKResponseContent
    {
        
        public A A { get; set; }
    } // end class

    public partial class PlaceType
    {
        
        public decimal Code { get; set; }
        public string Name { get; set; }
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
        
        public string Privacy { get; set; }
    } // end class

    public partial class TosMediaTypeExtensionGetOKResponseContent
    {
        
        public string Tos { get; set; }
    } // end class

    public partial class RateLimitContext
    {
        
        public string Access_token { get; set; }
    } // end class

    public partial class HelpConfiguration
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class HelpLanguages
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class HelpPrivacy
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class HelpTos
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class Help
    {
        
        public HelpConfiguration Helpconfiguration { get; set; }
        public HelpLanguages Helplanguages { get; set; }
        public HelpPrivacy Helpprivacy { get; set; }
        public HelpTos Helptos { get; set; }
    } // end class

    public partial class SearchTweets
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class Search
    {
        
        public SearchTweets Searchtweets { get; set; }
    } // end class

    public partial class StatusesHomeTimeline
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class StatusesMentionsTimeline
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class StatusesOembed
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class StatusesRetweetsId
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class StatusesShowId
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class StatusesUserTimeline
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class Statuses
    {
        
        public StatusesHomeTimeline Statuseshome_timeline { get; set; }
        public StatusesMentionsTimeline Statusesmentions_timeline { get; set; }
        public StatusesOembed Statusesoembed { get; set; }
        public StatusesRetweetsId Statusesretweetsid { get; set; }
        public StatusesShowId Statusesshowid { get; set; }
        public StatusesUserTimeline Statusesuser_timeline { get; set; }
    } // end class

    public partial class UsersContributees
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class UsersContributors
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class UsersLookup
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class UsersSearch
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class UsersShow
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class UsersSuggestionsSlugMembers
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class UsersSuggestionsSlug
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class UsersSuggestions
    {
        
        public decimal Limit { get; set; }
        public decimal Remaining { get; set; }
        public decimal Reset { get; set; }
    } // end class

    public partial class Users
    {
        
        public UsersContributees Userscontributees { get; set; }
        public UsersContributors Userscontributors { get; set; }
        public UsersLookup Userslookup { get; set; }
        public UsersSearch Userssearch { get; set; }
        public UsersShow Usersshow { get; set; }
        public UsersSuggestionsSlugMembers Userssuggestionsslugmembers { get; set; }
        public UsersSuggestionsSlug Userssuggestionsslug { get; set; }
        public UsersSuggestions Userssuggestions { get; set; }
    } // end class

    public partial class Resources
    {
        
        public Help Help { get; set; }
        public Search Search { get; set; }
        public Statuses Statuses { get; set; }
        public Users Users { get; set; }
    } // end class

    public partial class ApplicationRateLimitStatusMediaTypeExtensionGetOKResponseContent
    {
        
        public RateLimitContext Rate_limit_context { get; set; }
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