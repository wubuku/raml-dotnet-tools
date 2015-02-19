
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TwitterAPI.Objects;

namespace TwitterAPI
{
    public partial class StatusesController : ApiController
    {


        /// <summary>
		/// Returns the 20 most recent mentions (tweets containing a users&apos;s @screen_name)  for the authenticating user. The timeline returned is the equivalent of the one seen when you view your  mentions on twitter.com. This method can only return up to 800 tweets. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="trim_user">When set to either true, t or 1, each tweet returned in a timeline will  include a user object including only the status authors numerical ID.  Omit this parameter to receive the complete user object. </param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="count">Specifies the number of tweets to try and retrieve, up to a maximum of  200. The value of count is best thought of as a limit to the number of  tweets to return because suspended or deleted content is removed after  the count has been applied. We include retweets in the count, even if  include_rts is not supplied. It is recommended you always send include_rts=1  when using this API method. </param>
		/// <param name="since_id">Returns results with an ID greater than (that is, more recent than) the  specified ID. There are limits to the number of Tweets which can be accessed  through the API. If the limit of Tweets has occured since the since_id, the  since_id will be forced to the oldest ID available. </param>
		/// <param name="max_id">Returns results with an ID less than (that is, older than) or equal to  the specified ID. </param>
		/// <param name="contributor_details">This parameter enhances the contributors element of the status response  to include the screen_name of the contributor. By default only the user_id  of the contributor is included. </param>
        [HttpGet]
        [Route("mentions_timeline{mediaTypeExtension}")]
        public virtual IHttpActionResult GetBase([FromUri] string mediaTypeExtension,[FromUri] string trim_user = null,[FromUri] string include_entities = null,[FromUri] int? count = null,[FromUri] int? since_id = null,[FromUri] int? max_id = null,[FromUri] string contributor_details = null)
        {
            return Get(mediaTypeExtension,trim_user,include_entities,count,since_id,max_id,contributor_details);
        }


        /// <summary>
		/// Returns a collection of the most recent Tweets posted by the user indicated  by the screen_name or user_id parameters. User timelines belonging to protected users may only be requested when the  authenticated user either &quot;owns&quot; the timeline or is an approved follower of  the owner. The timeline returned is the equivalent of the one seen when you view a user&apos;s  profile on twitter.com. This method can only return up to 3,200 of a user&apos;s most recent Tweets. Native  retweets of other statuses by the user is included in this total, regardless  of whether include_rts is set to false when requesting this resource. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="trim_user">When set to either true, t or 1, each tweet returned in a timeline will  include a user object including only the status authors numerical ID.  Omit this parameter to receive the complete user object. </param>
		/// <param name="user_id">The ID of the user for whom to return results for.</param>
		/// <param name="screen_name">The screen name of the user for whom to return results for.</param>
		/// <param name="since_id">Returns results with an ID greater than (that is, more recent than) the  specified ID. There are limits to the number of Tweets which can be accessed  through the API. If the limit of Tweets has occured since the since_id, the  since_id will be forced to the oldest ID available. </param>
		/// <param name="count">Specifies the number of tweets to try and retrieve, up to a maximum of  200 per distinct request. The value of count is best thought of as a  limit to the number of tweets to return because suspended or deleted  content is removed after the count has been applied. We include retweets  in the count, even if include_rts is not supplied. It is recommended you  always send include_rts=1 when using this API method. </param>
		/// <param name="max_id">Returns results with an ID less than (that is, older than) or equal to  the specified ID. </param>
		/// <param name="contributor_details">This parameter enhances the contributors element of the status response  to include the screen_name of the contributor. By default only the user_id  of the contributor is included. </param>
		/// <param name="exclude_replies">This parameter will prevent replies from appearing in the returned timeline.  Using exclude_replies with the count parameter will mean you will receive  up-to count tweets - this is because the count parameter retrieves that  many tweets before filtering out retweets and replies. This parameter is  only supported for JSON and XML responses. </param>
		/// <param name="include_rts">When set to false, the timeline will strip any native retweets (though  they will still count toward both the maximal length of the timeline  and the slice selected by the count parameter). Note: If you&apos;re using  the trim_user parameter in conjunction with include_rts, the retweets  will still contain a full user object. </param>
        [HttpGet]
        [Route("user_timeline{mediaTypeExtension}")]
        public virtual IHttpActionResult GetUserTimelineByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] string trim_user = null,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? since_id = null,[FromUri] int? count = null,[FromUri] int? max_id = null,[FromUri] string contributor_details = null,[FromUri] string exclude_replies = null,[FromUri] string include_rts = null)
        {
            return GetUserTimelineByMediaTypeExtension(mediaTypeExtension,trim_user,user_id,screen_name,since_id,count,max_id,contributor_details,exclude_replies,include_rts);
        }


        /// <summary>
		/// Returns a collection of the most recent Tweets and retweets posted by the  authenticating user and the users they follow. The home timeline is central  to how most users interact with the Twitter service. Up to 800 Tweets are obtainable on the home timeline. It is more volatile  for users that follow many users or follow users who tweet frequently. See Working with Timelines for instructions on traversing timelines efficiently. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="trim_user">When set to either true, t or 1, each tweet returned in a timeline will  include a user object including only the status authors numerical ID.  Omit this parameter to receive the complete user object. </param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="count">Specifies the number of records to retrieve. Must be less than or equal  to 200. </param>
		/// <param name="since_id">Returns results with an ID greater than (that is, more recent than) the  specified ID. There are limits to the number of Tweets which can be accessed  through the API. If the limit of Tweets has occured since the since_id, the  since_id will be forced to the oldest ID available. </param>
		/// <param name="max_id">Returns results with an ID less than (that is, older than) or equal to  the specified ID. </param>
		/// <param name="exclude_replies">This parameter will prevent replies from appearing in the returned timeline.  Using exclude_replies with the count parameter will mean you will receive  up-to count tweets - this is because the count parameter retrieves that  many tweets before filtering out retweets and replies. This parameter is  only supported for JSON and XML responses. </param>
		/// <param name="contributor_details">This parameter enhances the contributors element of the status response  to include the screen_name of the contributor. By default only the user_id  of the contributor is included. </param>
        [HttpGet]
        [Route("home_timeline{mediaTypeExtension}")]
        public virtual IHttpActionResult GetHomeTimelineByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] string trim_user = null,[FromUri] string include_entities = null,[FromUri] int? count = null,[FromUri] int? since_id = null,[FromUri] int? max_id = null,[FromUri] string exclude_replies = null,[FromUri] string contributor_details = null)
        {
            return GetHomeTimelineByMediaTypeExtension(mediaTypeExtension,trim_user,include_entities,count,since_id,max_id,exclude_replies,contributor_details);
        }


        /// <summary>
		/// Returns the most recent tweets authored by the authenticating user that  have been retweeted by others. This timeline is a subset of the user&apos;s GET  statuses/user_timeline. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="trim_user">When set to either true, t or 1, each tweet returned in a timeline will  include a user object including only the status authors numerical ID.  Omit this parameter to receive the complete user object. </param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="count">Specifies the number of records to retrieve. Must be less than or equal  to 100. If omitted, 20 will be assumed. </param>
		/// <param name="since_id">Returns results with an ID greater than (that is, more recent than) the  specified ID. There are limits to the number of Tweets which can be accessed  through the API. If the limit of Tweets has occured since the since_id, the  since_id will be forced to the oldest ID available. </param>
		/// <param name="max_id">Returns results with an ID less than (that is, older than) or equal to  the specified ID. </param>
		/// <param name="include_user_entities">The user entities node will be disincluded when set to false.</param>
        [HttpGet]
        [Route("retweets_of_me{mediaTypeExtension}")]
        public virtual IHttpActionResult GetRetweetsOfMeByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] string trim_user = null,[FromUri] string include_entities = null,[FromUri] int? count = null,[FromUri] int? since_id = null,[FromUri] int? max_id = null,[FromUri] string include_user_entities = null)
        {
            return GetRetweetsOfMeByMediaTypeExtension(mediaTypeExtension,trim_user,include_entities,count,since_id,max_id,include_user_entities);
        }


        /// <summary>
		/// Returns a collection of the 100 most recent retweets of the tweet specified  by the id parameter. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="id">The numerical ID of the desired status.</param>
		/// <param name="trim_user">When set to either true, t or 1, each tweet returned in a timeline will  include a user object including only the status authors numerical ID.  Omit this parameter to receive the complete user object. </param>
		/// <param name="count">Specifies the number of records to retrieve. Must be less than or equal  to 100. </param>
        [HttpGet]
        [Route("retweets/{id}/{mediaTypeExtension}")]
        public virtual IHttpActionResult GetRetweetsByIdMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int id,[FromUri] string trim_user = null,[FromUri] int? count = null)
        {
            return GetRetweetsByIdMediaTypeExtension(mediaTypeExtension,id,trim_user,count);
        }


        /// <summary>
		/// Returns a single Tweet, specified by the id parameter. The Tweet&apos;s author  will also be embedded within the tweet.      Extended description About Geo If there is no geotag for a status, then there will be an empty &lt;geo/&gt; or  &quot;geo&quot; : {}. This can only be populated if the user has used the Geotagging  API to send a statuses/update. The JSON response mostly uses conventions laid out in GeoJSON. Unfortunately,  the coordinates that Twitter renders are reversed from the GeoJSON specification  (GeoJSON specifies a longitude then a latitude, whereas we are currently  representing it as a latitude then a longitude). Our JSON renders as:  ------------- &quot;geo&quot;: { &quot;type&quot;:&quot;Point&quot;, &quot;coordinates&quot;:[37.78029, -122.39697] }      Contributors If there are no contributors for a Tweet, then there will be an empty or  &quot;contributors&quot; : {}. This field will only be populated if the user has  contributors enabled on his or her account -- this is a beta feature that  is not yet generally available to all. This object contains an array of user IDs for users who have contributed  to this status (an example of a status that has been contributed to is this  one). In practice, there is usually only one ID in this array. The JSON  renders as such  ------------- &quot;contributors&quot;:[8285392]. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="id">The numerical ID of the desired Tweet.</param>
		/// <param name="trim_user">When set to either true, t or 1, each tweet returned in a timeline will  include a user object including only the status authors numerical ID.  Omit this parameter to receive the complete user object. </param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="include_my_retweet">When set to either true, t or 1, any Tweets returned that have been  retweeted by the authenticating user will include an additional  current_user_retweet node, containing the ID of the source status for  the retweet. </param>
        [HttpGet]
        [Route("show/{id}/{mediaTypeExtension}")]
        public virtual IHttpActionResult GetShowByIdMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int id,[FromUri] string trim_user = null,[FromUri] string include_entities = null,[FromUri] string include_my_retweet = null)
        {
            return GetShowByIdMediaTypeExtension(mediaTypeExtension,id,trim_user,include_entities,include_my_retweet);
        }


        /// <summary>
		/// Destroys the status specified by the required ID parameter. The authenticating  user must be the author of the specified status. Returns the destroyed status  if successful. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="id">The numerical ID of the desired status.</param>
        [HttpPost]
        [Route("destroy/{id}/{mediaTypeExtension}")]
        public virtual IHttpActionResult PostBase(string json,[FromUri] string mediaTypeExtension,[FromUri] int id)
        {
            return Post(json,mediaTypeExtension,id);
        }


        /// <summary>
		/// Updates the authenticating user&apos;s current status, also known as tweeting.  To upload an image to accompany the tweet, use `POST statuses/update_with_media`. For each update attempt, the update text is compared with the authenticating  user&apos;s recent tweets. Any attempt that would result in duplication will be  blocked, resulting in a 403 error. Therefore, a user cannot submit the same  status twice in a row. While not rate limited by the API a user is limited in the number of tweets  they can create at a time. If the number of updates posted by the user reaches  the current allowed limit this method will return an HTTP 403 error. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>UpdateMediaTypeExtensionPostOKResponseContent</returns>
        [ResponseType(typeof(UpdateMediaTypeExtensionPostOKResponseContent))]
        [HttpPost]
        [Route("update{mediaTypeExtension}")]
        public virtual IHttpActionResult PostUpdateByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostUpdateByMediaTypeExtension(json,mediaTypeExtension);
        }


        /// <summary>
		/// Retweets a tweet. Returns the original tweet with retweet details embedded. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="id">The numerical ID of the desired status.</param>
        [HttpPost]
        [Route("retweet/{id}/{mediaTypeExtension}")]
        public virtual IHttpActionResult PostRetweetByIdMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension,[FromUri] int id)
        {
            return PostRetweetByIdMediaTypeExtension(json,mediaTypeExtension,id);
        }


        /// <summary>
		/// Updates the authenticating user&apos;s current status and attaches media for  upload. In other words, it creates a Tweet with a picture attached. Unlike POST statuses/update, this method expects raw multipart data. Your  POST request&apos;s Content-Type should be set to multipart/form-data with the  media[] parameter The Tweet text will be rewritten to include the media URL(s), which will  reduce the number of characters allowed in the Tweet text. If the URL(s)  cannot be appended without text truncation, the tweet will be rejected and  this method will return an HTTP 403 error.  
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpPost]
        [Route("update_with_media{mediaTypeExtension}")]
        public virtual IHttpActionResult PostUpdateWithMediaByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostUpdateWithMediaByMediaTypeExtension(json,mediaTypeExtension);
        }


        /// <summary>
		/// Returns information allowing the creation of an embedded representation  of a Tweet on third party sites. See the oEmbed specification for information  about the response format. While this endpoint allows a bit of customization for the final appearance  of the embedded Tweet, be aware that the appearance of the rendered Tweet may  change over time to be consistent with Twitter&apos;s Display Requirements. Do not  rely on any class or id parameters to stay constant in the returned markup. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="id">The Tweet/status ID to return embed code for.</param>
		/// <param name="url">The URL of the Tweet/status to be embedded.</param>
		/// <param name="maxwidth">The maximum width in pixels that the embed should be rendered at. </param>
		/// <param name="hide_media">Specifies whether the embedded Tweet should automatically expand  images which were uploaded via POST statuses/update_with_media.  When set to either true, t or 1 images will not be expanded.  </param>
		/// <param name="hide_thread">Specifies whether the embedded Tweet should automatically show the  original message in the case that the embedded Tweet is a reply. When  set to either true, t or 1 the original Tweet will not be shown.  </param>
		/// <param name="omit_script">Specifies whether the embedded Tweet HTML should include a &lt;script&gt;  element pointing to widgets.js. In cases where a page already includes  widgets.js, setting this value to true will prevent a redundant script  element from being included. When set to either true, t or 1 the &lt;script&gt;  element will not be included in the embed HTML, meaning that pages must  include a reference to widgets.js manually. </param>
		/// <param name="align">Specifies whether the embedded Tweet should be left aligned, right aligned,  or centered in the page. Valid values are left, right, center, and none.  Defaults to none, meaning no alignment styles are specified for the Tweet. </param>
		/// <param name="related">A value for the TWT related parameter, as described in Web Intents. This  value will be forwarded to all Web Intents calls. </param>
		/// <param name="lang">Language code for the rendered embed. This will affect the text and  localization of the rendered HTML. </param>
        [HttpGet]
        [Route("oembed{mediaTypeExtension}")]
        public virtual IHttpActionResult GetOembedByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int id,[FromUri] string url,[FromUri] int? maxwidth = null,[FromUri] string hide_media = null,[FromUri] string hide_thread = null,[FromUri] string omit_script = null,[FromUri] string align = null,[FromUri] string related = null,[FromUri] string lang = null)
        {
            return GetOembedByMediaTypeExtension(mediaTypeExtension,id,url,maxwidth,hide_media,hide_thread,omit_script,align,related,lang);
        }


        /// <summary>
		/// Returns a collection of up to 100 user IDs belonging to users who have  retweeted the tweet specified by the id parameter. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="id">The numerical ID of the desired status.</param>
		/// <param name="cursor">Causes the list of IDs to be broken into pages of no more than 100 IDs  at a time. The number of IDs returned is not guaranteed to be 100 as  suspended users are filtered out after connections are queried. If no  cursor is provided, a value of -1 will be assumed, which is the first  &quot;page.&quot; The response from the API will include a previous_cursor and next_cursor to allow paging back and forth. See Using cursors to navigate collections for more information. While this method supports the cursor parameter, the entire result set can be returned in a single cursored collection. Using the count parameter with this method will not provide segmented cursors for use with this parameter. </param>
		/// <param name="stringify_ids">Many programming environments will not consume our ids due to their size.  Provide this option to have ids returned as strings instead. Read more  about Twitter IDs, JSON and Snowflake. </param>
        [HttpGet]
        [Route("retweeters/ids{mediaTypeExtension}")]
        public virtual IHttpActionResult GetRetweetersIdsByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int id,[FromUri] int? cursor = null,[FromUri] string stringify_ids = null)
        {
            return GetRetweetersIdsByMediaTypeExtension(mediaTypeExtension,id,cursor,stringify_ids);
        }


        /// <summary>
		/// Returns public statuses that match one or more filter predicates. Multiple  parameters may be specified which allows most clients to use a single connection  to the Streaming API. Both GET and POST requests are supported, but GET requests  with too many parameters may cause the request to be rejected for excessive URL  length. Use a POST request to avoid long URLs. The track, follow, and locations fields should be considered to be combined  with an OR operator. track=foo&amp;follow=1234 returns Tweets matching &quot;foo&quot; OR  created by user 1234. The default access level allows up to 400 track keywords, 5,000 follow userids  and 25 0.1-360 degree location boxes. If you need elevated access to the Streaming  API, you should explore our partner providers of Twitter data here. Note: At least one predicate parameter (follow, locations, or track) must be specified. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpPost]
        [Route("filter{mediaTypeExtension}")]
        public virtual IHttpActionResult PostFilterByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostFilterByMediaTypeExtension(json,mediaTypeExtension);
        }


        /// <summary>
		/// Returns a small random sample of all public statuses. The Tweets returned  by the default access level are the same, so if two different clients connect  to this endpoint, they will see the same Tweets. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="delimited">Specifies whether messages should be length-delimited. See the delimited  parameter documentation for more information. </param>
		/// <param name="stall_warnings">Specifies whether stall warnings should be delivered. See the stall_warnings  parameter documentation for more information. </param>
        [HttpGet]
        [Route("sample{mediaTypeExtension}")]
        public virtual IHttpActionResult GetSampleByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] string delimited = null,[FromUri] string stall_warnings = null)
        {
            return GetSampleByMediaTypeExtension(mediaTypeExtension,delimited,stall_warnings);
        }


        /// <summary>
		/// This endpoint requires special permission to access. Returns all public statuses. Few applications require this level of access.  Creative use of a combination of other resources and various access levels  can satisfy nearly every application use case. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="count">The number of messages to backfill. See the count parameter documentation  for more information. </param>
		/// <param name="delimited">Specifies whether messages should be length-delimited. See the delimited  parameter documentation for more information. </param>
		/// <param name="stall_warnings">Specifies whether stall warnings should be delivered. See the stall_warnings  parameter documentation for more information. </param>
        [HttpGet]
        [Route("firehose{mediaTypeExtension}")]
        public virtual IHttpActionResult GetFirehoseByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int? count = null,[FromUri] string delimited = null,[FromUri] string stall_warnings = null)
        {
            return GetFirehoseByMediaTypeExtension(mediaTypeExtension,count,delimited,stall_warnings);
        }

    }

    public partial class SearchTweetsMediaTypeExtensionController : ApiController
    {


        /// <summary>
		/// Returns a collection of relevant Tweets matching a specified query. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="q">A UTF-8, URL-encoded search query of 1,000 characters maximum,  including operators. Queries may additionally be limited by complexity. </param>
		/// <param name="geocode">Returns tweets by users located within a given radius of the given  latitude/longitude. The location is preferentially taking from the  Geotagging API, but will fall back to their Twitter profile. The  parameter value is specified by &quot;latitude,longitude,radius&quot;, where  radius units must be specified as either &quot;mi&quot; (miles) or &quot;km&quot; (kilometers).  Note that you cannot use the near operator via the API to geocode arbitrary  locations; however you can use this geocode parameter to search near geocodes  directly. A maximum of 1,000 distinct &quot;sub-regions&quot; will be considered when  using the radius modifier. </param>
		/// <param name="lang">Restricts tweets to the given language, given by an ISO 639-1 code.  Language detection is best-effort. </param>
		/// <param name="locale">Specify the language of the query you are sending (only ja is currently  effective). This is intended for language-specific consumers and the  default should work in the majority of cases. </param>
		/// <param name="result_type">Specifies what type of search results you would prefer to receive. The  current default is &quot;mixed.&quot; Valid values include  * mixed: Include both popular and real time results in the response.  * recent: return only the most recent results in the response  * popular: return only the most popular results in the response. </param>
		/// <param name="count">The number of tweets to return per page, up to a maximum of 100. </param>
		/// <param name="until">Returns tweets generated before the given date. Date should be formatted  as YYYY-MM-DD. Keep in mind that the search index may not go back as far  as the date you specify here. </param>
		/// <param name="since_id">Returns results with an ID greater than (that is, more recent than) the  specified ID. There are limits to the number of Tweets which can be  accessed through the API. If the limit of Tweets has occured since the  since_id, the since_id will be forced to the oldest ID available. </param>
		/// <param name="max_id">Returns results with an ID less than (that is, older than) or equal to  the specified ID. </param>
		/// <param name="callback">If supplied, the response will use the JSONP format with a callback of  the given name. The usefulness of this parameter is somewhat diminished  by the requirement of authentication for requests to this endpoint. </param>
        [HttpGet]
        [Route("{mediaTypeExtension}")]
        public virtual IHttpActionResult GetBase([FromUri] string mediaTypeExtension,[FromUri] string q,[FromUri] string include_entities = null,[FromUri] string geocode = null,[FromUri] string lang = null,[FromUri] string locale = null,[FromUri] string result_type = null,[FromUri] int? count = null,[FromUri] string until = null,[FromUri] int? since_id = null,[FromUri] int? max_id = null,[FromUri] string callback = null)
        {
            return Get(mediaTypeExtension,q,include_entities,geocode,lang,locale,result_type,count,until,since_id,max_id,callback);
        }

    }

    public partial class DirectMessagesMediaTypeExtensionController : ApiController
    {


        /// <summary>
		/// Returns the 20 most recent direct messages sent to the authenticating user.  Includes detailed information about the sender and recipient user. You can  request up to 200 direct messages per call, up to a maximum of 800 incoming DMs. Important: This method requires an access token with RWD (read, write &amp; direct  message) permissions. Consult The Application Permission Model for more  information. (https://dev.twitter.com/docs/application-permission-model) 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="since_id">Returns results with an ID greater than (that is, more recent than) the  specified ID. There are limits to the number of Tweets which can be  accessed through the API. If the limit of Tweets has occured since the  since_id, the since_id will be forced to the oldest ID available. </param>
		/// <param name="max_id">Returns results with an ID less than (that is, older than) or equal to  the specified ID. </param>
		/// <param name="count">Specifies the number of direct messages to try and retrieve, up to a  maximum of 200. The value of count is best thought of as a limit to the  number of Tweets to return because suspended or deleted content is removed  after the count has been applied.  </param>
		/// <param name="skip_status">When set to either true, t or 1 statuses will not be included in the  returned user objects. </param>
        [HttpGet]
        [Route("{mediaTypeExtension}")]
        public virtual IHttpActionResult GetBase([FromUri] string mediaTypeExtension,[FromUri] string include_entities = null,[FromUri] int? since_id = null,[FromUri] int? max_id = null,[FromUri] int? count = null,[FromUri] string skip_status = null)
        {
            return Get(mediaTypeExtension,include_entities,since_id,max_id,count,skip_status);
        }


        /// <summary>
		/// Returns the 20 most recent direct messages sent by the authenticating user.  Includes detailed information about the sender and recipient user. You can  request up to 200 direct messages per call, up to a maximum of 800 outgoing DMs. Important: This method requires an access token with RWD (read, write &amp;  direct message) permissions. Consult The Application Permission Model for  more information. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="since_id">Returns results with an ID greater than (that is, more recent than) the  specified ID. There are limits to the number of Tweets which can be  accessed through the API. If the limit of Tweets has occured since the  since_id, the since_id will be forced to the oldest ID available. </param>
		/// <param name="max_id">Returns results with an ID less than (that is, older than) or equal to  the specified ID. </param>
		/// <param name="count">Specifies the number of direct messages to try and retrieve, up to a  maximum of 200. The value of count is best thought of as a limit to the  number of Tweets to return because suspended or deleted content is removed  after the count has been applied.  </param>
		/// <param name="page">Specifies the page of results to retrieve.</param>
        [HttpGet]
        [Route("sent{mediaTypeExtension}")]
        public virtual IHttpActionResult GetSentByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] string include_entities = null,[FromUri] int? since_id = null,[FromUri] int? max_id = null,[FromUri] int? count = null,[FromUri] int? page = null)
        {
            return GetSentByMediaTypeExtension(mediaTypeExtension,include_entities,since_id,max_id,count,page);
        }


        /// <summary>
		/// Returns a single direct message, specified by an id parameter. Like the  /1.1/direct_messages.format request, this method will include the user  objects of the sender and recipient.  Important: This method requires an access token with RWD (read, write &amp;  direct message) permissions. Consult The Application Permission Model for  more information. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="id">The ID of the direct message.</param>
		/// <returns>ShowMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(ShowMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("show{mediaTypeExtension}")]
        public virtual IHttpActionResult GetShowByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int id)
        {
            return GetShowByMediaTypeExtension(mediaTypeExtension,id);
        }


        /// <summary>
		/// Destroys the direct message specified in the required ID parameter. The  authenticating user must be the recipient of the specified direct message. Important: This method requires an access token with RWD (read, write &amp;  direct message) permissions. Consult The Application Permission Model for  more information. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>DestroyMediaTypeExtensionPostOKResponseContent</returns>
        [ResponseType(typeof(DestroyMediaTypeExtensionPostOKResponseContent))]
        [HttpPost]
        [Route("destroy{mediaTypeExtension}")]
        public virtual IHttpActionResult PostBase(string json,[FromUri] string mediaTypeExtension)
        {
            return Post(json,mediaTypeExtension);
        }


        /// <summary>
		/// Sends a new direct message to the specified user from the authenticating user.  Requires both the user and text parameters and must be a POST. Returns the  sent message in the requested format if successful. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpPost]
        [Route("new{mediaTypeExtension}")]
        public virtual IHttpActionResult PostNewByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostNewByMediaTypeExtension(json,mediaTypeExtension);
        }

    }

    public partial class FriendshipsMediaTypeExtensionController : ApiController
    {


        /// <summary>
		/// Returns a collection of user_ids that the currently authenticated user does  not want to receive retweets from. Use POST friendships/update to set the &quot;no retweets&quot; status for a given user  account on behalf of the current user. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="stringify_ids">Many programming environments will not consume our ids due to their size.  Provide this option to have ids returned as strings instead. Read more  about Twitter IDs, JSON and Snowflake. This parameter is especially  important to use in Javascript environments. </param>
        [HttpGet]
        [Route("no_retweets/ids{mediaTypeExtension}")]
        public virtual IHttpActionResult GetBase([FromUri] string mediaTypeExtension,[FromUri] string stringify_ids = null)
        {
            return Get(mediaTypeExtension,stringify_ids);
        }


        /// <summary>
		/// Returns the relationships of the authenticating user to the comma-separated  list of up to 100 screen_names or user_ids provided. Values for connections  can be: following, following_requested, followed_by, none. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="screen_name">A comma separated list of screen names, up to 100 are allowed in a single  request. </param>
		/// <param name="user_id">A comma separated list of user IDs, up to 100 are allowed in a single  request. </param>
        [HttpGet]
        [Route("lookup{mediaTypeExtension}")]
        public virtual IHttpActionResult GetLookupByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] string screen_name = null,[FromUri] string user_id = null)
        {
            return GetLookupByMediaTypeExtension(mediaTypeExtension,screen_name,user_id);
        }


        /// <summary>
		/// Returns a collection of numeric IDs for every user who has a pending request  to follow the authenticating user. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="cursor">Causes the list of connections to be broken into pages of no more than  5000 IDs at a time. The number of IDs returned is not guaranteed to be  5000 as suspended users are filtered out after connections are queried.  If no cursor is provided, a value of -1 will be assumed, which is the  first &quot;page.&quot; The response from the API will include a previous_cursor and next_cursor  to allow paging back and forth. See Using cursors to navigate collections  for more information. </param>
		/// <param name="stringify_ids">Many programming environments will not consume our Tweet ids due to their  size. Provide this option to have ids returned as strings instead. More  about Twitter IDs, JSON and Snowflake. </param>
        [HttpGet]
        [Route("incoming{mediaTypeExtension}")]
        public virtual IHttpActionResult GetIncomingByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int? cursor = null,[FromUri] string stringify_ids = null)
        {
            return GetIncomingByMediaTypeExtension(mediaTypeExtension,cursor,stringify_ids);
        }


        /// <summary>
		/// Returns a collection of numeric IDs for every protected user for whom the  authenticating user has a pending follow request. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="cursor">Causes the list of connections to be broken into pages of no more than  5000 IDs at a time. The number of IDs returned is not guaranteed to be  5000 as suspended users are filtered out after connections are queried.  If no cursor is provided, a value of -1 will be assumed, which is the  first &quot;page.&quot; The response from the API will include a previous_cursor and next_cursor  to allow paging back and forth. See Using cursors to navigate collections  for more information. </param>
		/// <param name="stringify_ids">Many programming environments will not consume our Tweet ids due to their  size. Provide this option to have ids returned as strings instead. More  about Twitter IDs, JSON and Snowflake. </param>
        [HttpGet]
        [Route("outgoing{mediaTypeExtension}")]
        public virtual IHttpActionResult GetOutgoingByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int? cursor = null,[FromUri] string stringify_ids = null)
        {
            return GetOutgoingByMediaTypeExtension(mediaTypeExtension,cursor,stringify_ids);
        }


        /// <summary>
		/// Allows the authenticating users to follow the user specified in the ID  parameter. Returns the befriended user in the requested format when successful. Returns  a string describing the failure condition when unsuccessful. If you are  already friends with the user a HTTP 403 may be returned, though for performance  reasons you may get a 200 OK message even if the friendship already exists. Actions taken in this method are asynchronous and changes will be eventually  consistent. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>CreateMediaTypeExtensionPostOKResponseContent</returns>
        [ResponseType(typeof(CreateMediaTypeExtensionPostOKResponseContent))]
        [HttpPost]
        [Route("create{mediaTypeExtension}")]
        public virtual IHttpActionResult PostBase(string json,[FromUri] string mediaTypeExtension)
        {
            return Post(json,mediaTypeExtension);
        }


        /// <summary>
		/// Allows the authenticating user to unfollow the user specified in the ID  parameter. Returns the unfollowed user in the requested format when successful. Returns  a string describing the failure condition when unsuccessful. Actions taken in this method are asynchronous and changes will be eventually  consistent. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>DestroyMediaTypeExtensionPostOKResponseContent</returns>
        [ResponseType(typeof(DestroyMediaTypeExtensionPostOKResponseContent))]
        [HttpPost]
        [Route("destroy{mediaTypeExtension}")]
        public virtual IHttpActionResult PostDestroyByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostDestroyByMediaTypeExtension(json,mediaTypeExtension);
        }


        /// <summary>
		/// Allows one to enable or disable retweets and device notifications from the specified user. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>UpdateMediaTypeExtensionPostOKResponseContent</returns>
        [ResponseType(typeof(UpdateMediaTypeExtensionPostOKResponseContent))]
        [HttpPost]
        [Route("update{mediaTypeExtension}")]
        public virtual IHttpActionResult PostUpdateByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostUpdateByMediaTypeExtension(json,mediaTypeExtension);
        }


        /// <summary>
		/// Returns detailed information about the relationship between two arbitrary  users. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="source_id">The user_id of the subject user.</param>
		/// <param name="source_screen_name">The screen_name of the subject user.</param>
		/// <param name="target_id">The user_id of the target user.</param>
		/// <param name="target_screen_name">The screen_name of the target user.</param>
		/// <returns>ShowMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(ShowMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("show{mediaTypeExtension}")]
        public virtual IHttpActionResult GetShowByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int? source_id = null,[FromUri] string source_screen_name = null,[FromUri] int? target_id = null,[FromUri] string target_screen_name = null)
        {
            return GetShowByMediaTypeExtension(mediaTypeExtension,source_id,source_screen_name,target_id,target_screen_name);
        }

    }

    public partial class FriendsController : ApiController
    {


        /// <summary>
		/// Returns a cursored collection of user IDs for every user the specified user  is following (otherwise known as their &quot;friends&quot;). At this time, results are ordered with the most recent following first - however,  this ordering is subject to unannounced change and eventual consistency issues.  Results are given in groups of 5,000 user IDs and multiple &quot;pages&quot; of results  can be navigated through using the next_cursor value in subsequent requests.  See Using cursors to navigate collections for more information. This method is especially powerful when used in conjunction with  &apos;GET users/lookup&apos;, a method that allows you to convert user IDs into full  user objects in bulk. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="user_id">The ID of the user for whom to return results for.</param>
		/// <param name="screen_name">The screen name of the user for whom to return results for.</param>
		/// <param name="cursor">Causes the list of connections to be broken into pages of no more than  5000 IDs at a time. The number of IDs returned is not guaranteed to be  5000 as suspended users are filtered out after connections are queried.  If no cursor is provided, a value of -1 will be assumed, which is the  first &quot;page.&quot; The response from the API will include a previous_cursor and next_cursor  to allow paging back and forth. See Using cursors to navigate collections  for more information. </param>
		/// <param name="stringify_ids">Many programming environments will not consume our Tweet ids due to their  size. Provide this option to have ids returned as strings instead. More  about Twitter IDs, JSON and Snowflake. </param>
		/// <param name="count">Specifies the number of IDs attempt retrieval of, up to a maximum of 5,000  per distinct request. The value of count is best thought of as a limit to  the number of results to return. When using the count parameter with this  method, it is wise to use a consistent count value across all requests to  the same user&apos;s collection. Usage of this parameter is encouraged in  environments where all 5,000 IDs constitutes too large of a response. </param>
        [HttpGet]
        [Route("ids{mediaTypeExtension}")]
        public virtual IHttpActionResult GetBase([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? cursor = null,[FromUri] string stringify_ids = null,[FromUri] int? count = null)
        {
            return Get(mediaTypeExtension,user_id,screen_name,cursor,stringify_ids,count);
        }


        /// <summary>
		/// Returns a cursored collection of user objects for every user the specified  user is following (otherwise known as their &quot;friends&quot;). At this time, results are ordered with the most recent following first -  however, this ordering is subject to unannounced change and eventual consistency  issues. Results are given in groups of 20 users and multiple &quot;pages&quot; of results  can be navigated through using the next_cursor value in subsequent requests.  See Using cursors to navigate collections for more information. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="user_id">The ID of the user for whom to return results for.</param>
		/// <param name="screen_name">The screen name of the user for whom to return results for.</param>
		/// <param name="cursor">Causes the list of connections to be broken into pages of no more than  5000 IDs at a time. The number of IDs returned is not guaranteed to be  5000 as suspended users are filtered out after connections are queried.  If no cursor is provided, a value of -1 will be assumed, which is the  first &quot;page.&quot; The response from the API will include a previous_cursor and next_cursor  to allow paging back and forth. See Using cursors to navigate collections  for more information. </param>
		/// <param name="skip_status">When set to either true, t or 1 statuses will not be included in the  returned user objects. </param>
		/// <param name="include_user_entities">The user object entities node will be disincluded when set to false. </param>
		/// <returns>ListMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(ListMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("list{mediaTypeExtension}")]
        public virtual IHttpActionResult GetListByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? cursor = null,[FromUri] string skip_status = null,[FromUri] string include_user_entities = null)
        {
            return GetListByMediaTypeExtension(mediaTypeExtension,user_id,screen_name,cursor,skip_status,include_user_entities);
        }

    }

    public partial class FollowersController : ApiController
    {


        /// <summary>
		/// Returns a cursored collection of user IDs for every user following the  specified user. At this time, results are ordered with the most recent following first -  however, this ordering is subject to unannounced change and eventual  consistency issues. Results are given in groups of 5,000 user IDs and  multiple &quot;pages&quot; of results can be navigated through using the next_cursor  value in subsequent requests. See Using cursors to navigate collections  for more information. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="user_id">The ID of the user for whom to return results for.</param>
		/// <param name="screen_name">The screen name of the user for whom to return results for.</param>
		/// <param name="cursor">Causes the list of connections to be broken into pages of no more than  5000 IDs at a time. The number of IDs returned is not guaranteed to be  5000 as suspended users are filtered out after connections are queried.  If no cursor is provided, a value of -1 will be assumed, which is the  first &quot;page.&quot; The response from the API will include a previous_cursor and next_cursor  to allow paging back and forth. See Using cursors to navigate collections  for more information. </param>
		/// <param name="stringify_ids">Many programming environments will not consume our Tweet ids due to their  size. Provide this option to have ids returned as strings instead. More  about Twitter IDs, JSON and Snowflake. </param>
		/// <param name="count">Specifies the number of IDs attempt retrieval of, up to a maximum of 5,000  per distinct request. The value of count is best thought of as a limit to  the number of results to return. When using the count parameter with this  method, it is wise to use a consistent count value across all requests to  the same user&apos;s collection. Usage of this parameter is encouraged in  environments where all 5,000 IDs constitutes too large of a response. </param>
        [HttpGet]
        [Route("ids{mediaTypeExtension}")]
        public virtual IHttpActionResult GetBase([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? cursor = null,[FromUri] string stringify_ids = null,[FromUri] int? count = null)
        {
            return Get(mediaTypeExtension,user_id,screen_name,cursor,stringify_ids,count);
        }


        /// <summary>
		/// Returns a cursored collection of user objects for users following the  specified user. At this time, results are ordered with the most recent following first -  however, this ordering is subject to unannounced change and eventual  consistency issues. Results are given in groups of 20 users and multiple  &quot;pages&quot; of results can be navigated through using the next_cursor value in  subsequent requests. See Using cursors to navigate collections for more  information. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="user_id">The ID of the user for whom to return results for.</param>
		/// <param name="screen_name">The screen name of the user for whom to return results for.</param>
		/// <param name="cursor">Causes the list of connections to be broken into pages of no more than  5000 IDs at a time. The number of IDs returned is not guaranteed to be  5000 as suspended users are filtered out after connections are queried.  If no cursor is provided, a value of -1 will be assumed, which is the  first &quot;page.&quot; The response from the API will include a previous_cursor and next_cursor  to allow paging back and forth. See Using cursors to navigate collections  for more information. </param>
		/// <param name="skip_status">When set to either true, t or 1 statuses will not be included in the  returned user objects. </param>
		/// <param name="include_user_entities">The user object entities node will be disincluded when set to false. </param>
		/// <returns>ListMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(ListMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("list{mediaTypeExtension}")]
        public virtual IHttpActionResult GetListByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? cursor = null,[FromUri] string skip_status = null,[FromUri] string include_user_entities = null)
        {
            return GetListByMediaTypeExtension(mediaTypeExtension,user_id,screen_name,cursor,skip_status,include_user_entities);
        }

    }

    public partial class AccountController : ApiController
    {


        /// <summary>
		/// Returns settings (including current trend, geo and sleep time information)  for the authenticating user. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpGet]
        [Route("settings{mediaTypeExtension}")]
        public virtual IHttpActionResult GetBase([FromUri] string mediaTypeExtension)
        {
            return Get(mediaTypeExtension);
        }


        /// <summary>
		/// Updates the authenticating user&apos;s settings. While all parameters for this method are optional, at least one or more  should be provided when executing this request. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpPost]
        [Route("settings{mediaTypeExtension}")]
        public virtual IHttpActionResult PostBase(string json,[FromUri] string mediaTypeExtension)
        {
            return Post(json,mediaTypeExtension);
        }


        /// <summary>
		/// Returns an HTTP 200 OK response code and a representation of the requesting  user if authentication was successful; returns a 401 status code and an error  message if not. Use this method to test if supplied user credentials are valid. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="skip_status">When set to either true, t or 1 statuses will not be included in the  returned user objects. </param>
        [HttpGet]
        [Route("verify_credentials{mediaTypeExtension}")]
        public virtual IHttpActionResult GetVerifyCredentialsByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] string include_entities = null,[FromUri] string skip_status = null)
        {
            return GetVerifyCredentialsByMediaTypeExtension(mediaTypeExtension,include_entities,skip_status);
        }


        /// <summary>
		/// Sets which device Twitter delivers updates to for the authenticating user.  Sending none as the device parameter will disable SMS updates. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpPost]
        [Route("update_delivery_device{mediaTypeExtension}")]
        public virtual IHttpActionResult PostUpdateDeliveryDeviceByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostUpdateDeliveryDeviceByMediaTypeExtension(json,mediaTypeExtension);
        }


        /// <summary>
		/// Sets values that users are able to set under the &quot;Account&quot; tab of their  settings page. Only the parameters specified will be updated. While no specific parameter is required, at least one of these parameters  should be provided when executing this method. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpPost]
        [Route("update_profile{mediaTypeExtension}")]
        public virtual IHttpActionResult PostUpdateProfileByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostUpdateProfileByMediaTypeExtension(json,mediaTypeExtension);
        }


        /// <summary>
		/// Updates the authenticating user&apos;s profile background image. This method can  also be used to enable or disable the profile background image. Although each parameter is marked as optional, at least one of image, tile  or use must be provided when making this request. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>UpdateProfileBackgroundImageMediaTypeExtensionPostOKResponseContent</returns>
        [ResponseType(typeof(UpdateProfileBackgroundImageMediaTypeExtensionPostOKResponseContent))]
        [HttpPost]
        [Route("update_profile_background_image{mediaTypeExtension}")]
        public virtual IHttpActionResult PostUpdateProfileBackgroundImageByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostUpdateProfileBackgroundImageByMediaTypeExtension(json,mediaTypeExtension);
        }


        /// <summary>
		/// Sets one or more hex values that control the color scheme of the authenticating  user&apos;s profile page on twitter.com. Each parameter&apos;s value must be a valid  hexidecimal value, and may be either three or six characters (ex: #fff or #ffffff). 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpPost]
        [Route("update_profile_colors{mediaTypeExtension}")]
        public virtual IHttpActionResult PostUpdateProfileColorsByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostUpdateProfileColorsByMediaTypeExtension(json,mediaTypeExtension);
        }


        /// <summary>
		/// Updates the authenticating user&apos;s profile image. Note that this method  expects raw multipart data, not a URL to an image. This method asynchronously processes the uploaded file before updating the  user&apos;s profile image URL. You can either update your local cache the next  time you request the user&apos;s information, or, at least 5 seconds after  uploading the image, ask for the updated URL using GET users/show. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpPost]
        [Route("update_profile_image{mediaTypeExtension}")]
        public virtual IHttpActionResult PostUpdateProfileImageByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostUpdateProfileImageByMediaTypeExtension(json,mediaTypeExtension);
        }


        /// <summary>
		/// Uploads a profile banner on behalf of the authenticating user. For best  results, upload an &lt;5MB image that is exactly 1252px by 626px. Images will  be resized for a number of display options. Users with an uploaded profile  banner will have a profile_banner_url node in their Users objects. More  information about sizing variations can be found in User Profile Images  and Banners and GET users/profile_banner.  Profile banner images are processed asynchronously. The profile_banner_url  and its variant sizes will not necessary be available directly after upload.  Note: If providing any one of the height, width, offset_left, or offset_top   parameters, you must provide all of the sizing parameters. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpPost]
        [Route("update_profile_banner{mediaTypeExtension}")]
        public virtual IHttpActionResult PostUpdateProfileBannerByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostUpdateProfileBannerByMediaTypeExtension(json,mediaTypeExtension);
        }


        /// <summary>
		/// Removes the uploaded profile banner for the authenticating user. Returns  HTTP 200 upon success. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpPost]
        [Route("remove_profile_banner{mediaTypeExtension}")]
        public virtual IHttpActionResult PostRemoveProfileBannerByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostRemoveProfileBannerByMediaTypeExtension(json,mediaTypeExtension);
        }

    }

    public partial class BlocksController : ApiController
    {


        /// <summary>
		/// Returns a collection of user objects that the authenticating user is blocking. Important On October 15, 2012 this method will become cursored by default,  altering the default response format. See Using cursors to navigate collections  for more details on how cursoring works. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="skip_status">When set to either true, t or 1 statuses will not be included in the  returned user objects. </param>
		/// <param name="cursor">Causes the list of blocked users to be broken into pages of no more than  5000 IDs at a time. The number of IDs returned is not guaranteed to be  5000 as suspended users are filtered out after connections are queried.  If no cursor is provided, a value of -1 will be assumed, which is the  first &quot;page.&quot; The response from the API will include a previous_cursor and next_cursor  to allow paging back and forth. See Using cursors to navigate collections  for more information. </param>
		/// <returns>ListMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(ListMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("list{mediaTypeExtension}")]
        public virtual IHttpActionResult GetBase([FromUri] string mediaTypeExtension,[FromUri] string include_entities = null,[FromUri] string skip_status = null,[FromUri] int? cursor = null)
        {
            return Get(mediaTypeExtension,include_entities,skip_status,cursor);
        }


        /// <summary>
		/// Returns an array of numeric user ids the authenticating user is blocking. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="stringify_ids">Many programming environments will not consume our ids due to their  size. Provide this option to have ids returned as strings instead.  Read more about Twitter IDs, JSON and Snowflake. </param>
		/// <param name="cursor">Causes the list of IDs to be broken into pages of no more than 5000 IDs  at a time. The number of IDs returned is not guaranteed to be 5000 as  suspended users are filtered out after connections are queried. If no  cursor is provided, a value of -1 will be assumed, which is the first  &quot;page.&quot; The response from the API will include a previous_cursor and next_cursor  to allow paging back and forth. See Using cursors to navigate collections  for more information. </param>
        [HttpGet]
        [Route("ids{mediaTypeExtension}")]
        public virtual IHttpActionResult GetIdsByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] string stringify_ids = null,[FromUri] int? cursor = null)
        {
            return GetIdsByMediaTypeExtension(mediaTypeExtension,stringify_ids,cursor);
        }


        /// <summary>
		/// Blocks the specified user from following the authenticating user. In addition  the blocked user will not show in the authenticating users mentions or timeline  (unless retweeted by another user). If a follow or friend relationship exists  it is destroyed. Either screen_name or user_id must be provided. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>CreateMediaTypeExtensionPostOKResponseContent</returns>
        [ResponseType(typeof(CreateMediaTypeExtensionPostOKResponseContent))]
        [HttpPost]
        [Route("create{mediaTypeExtension}")]
        public virtual IHttpActionResult PostBase(string json,[FromUri] string mediaTypeExtension)
        {
            return Post(json,mediaTypeExtension);
        }


        /// <summary>
		/// Un-blocks the user specified in the ID parameter for the authenticating user.  Returns the un-blocked user in the requested format when successful. If  relationships existed before the block was instated, they will not be restored. One of screen_name or id must be provided. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>DestroyMediaTypeExtensionPostOKResponseContent</returns>
        [ResponseType(typeof(DestroyMediaTypeExtensionPostOKResponseContent))]
        [HttpPost]
        [Route("destroy{mediaTypeExtension}")]
        public virtual IHttpActionResult PostDestroyByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostDestroyByMediaTypeExtension(json,mediaTypeExtension);
        }

    }

    public partial class UsersController : ApiController
    {


        /// <summary>
		/// Returns fully-hydrated user objects for up to 100 users per request, as  specified by comma-separated values passed to the user_id and/or  screen_name parameters. This method is especially useful when used in conjunction with collections  of user IDs returned from GET friends/ids and GET followers/ids. GET users/show is used to retrieve a single user object. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="screen_name">A comma separated list of screen names, up to 100 are allowed in a single  request. You are strongly encouraged to use a POST for larger (up to  100 screen names) requests. </param>
		/// <param name="user_id">A comma separated list of user IDs, up to 100 are allowed in a single  request. You are strongly encouraged to use a POST for larger requests. </param>
        [HttpGet]
        [Route("lookup{mediaTypeExtension}")]
        public virtual IHttpActionResult GetBase([FromUri] string mediaTypeExtension,[FromUri] string include_entities = null,[FromUri] string screen_name = null,[FromUri] string user_id = null)
        {
            return Get(mediaTypeExtension,include_entities,screen_name,user_id);
        }


        /// <summary>
		/// Returns a variety of information about the user specified by the required  user_id or screen_name parameter. The author&apos;s most recent Tweet will be  returned inline when possible. GET users/lookup is used to retrieve a bulk collection of user objects. You must be following a protected user to be able to see their most recent  Tweet. If you don&apos;t follow a protected user, the users Tweet will be removed.  A Tweet will not always be returned in the current_status field. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="user_id">The ID of the user for whom to return results for. Either an id or  screen_name is required for this method. </param>
		/// <param name="screen_name">The screen name of the user for whom to return results for. Either  a id or screen_name is required for this method. </param>
		/// <returns>ShowMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(ShowMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("show{mediaTypeExtension}")]
        public virtual IHttpActionResult GetShowByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int user_id,[FromUri] string include_entities = null,[FromUri] string screen_name = null)
        {
            return GetShowByMediaTypeExtension(mediaTypeExtension,user_id,include_entities,screen_name);
        }


        /// <summary>
		/// Provides a simple, relevance-based search interface to public user accounts  on Twitter. Try querying by topical interest, full name, company name,  location, or other criteria. Exact match searches are not supported. Only the first 1,000 matching results are available. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="q">The search query to run against people search. </param>
		/// <param name="page">Specifies the page of results to retrieve. </param>
		/// <param name="count">The number of potential user results to retrieve per page. This value  has a maximum of 20. </param>
		/// <returns>SearchMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(SearchMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("search{mediaTypeExtension}")]
        public virtual IHttpActionResult GetSearchByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] string q,[FromUri] string include_entities = null,[FromUri] string page = null,[FromUri] int? count = null)
        {
            return GetSearchByMediaTypeExtension(mediaTypeExtension,q,include_entities,page,count);
        }


        /// <summary>
		/// Returns a collection of users that the specified user can &quot;contribute&quot; to. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="user_id">The ID of the user for whom to return results for. Helpful for disambiguating  when a valid user ID is also a valid screen name. </param>
		/// <param name="screen_name">The screen name of the user for whom to return results for. </param>
		/// <param name="skip_status">When set to either true, t or 1 statuses will not be included in the  returned user objects. </param>
        [HttpGet]
        [Route("contributees{mediaTypeExtension}")]
        public virtual IHttpActionResult GetContributeesByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] string include_entities = null,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] string skip_status = null)
        {
            return GetContributeesByMediaTypeExtension(mediaTypeExtension,include_entities,user_id,screen_name,skip_status);
        }


        /// <summary>
		/// Returns a collection of users who can contribute to the specified account. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="user_id">The ID of the user for whom to return results for. </param>
		/// <param name="screen_name">The screen name of the user for whom to return results for. </param>
		/// <param name="skip_status">When set to either true, t or 1 statuses will not be included in the  returned user objects. </param>
        [HttpGet]
        [Route("contributors{mediaTypeExtension}")]
        public virtual IHttpActionResult GetContributorsByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] string include_entities = null,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] string skip_status = null)
        {
            return GetContributorsByMediaTypeExtension(mediaTypeExtension,include_entities,user_id,screen_name,skip_status);
        }


        /// <summary>
		/// Returns a map of the available size variations of the specified user&apos;s profile  banner. If the user has not uploaded a profile banner, a HTTP 404 will be  served instead. This method can be used instead of string manipulation on the  profile_banner_url returned in user objects as described in User Profile Images  and Banners. The profile banner data available at each size variant&apos;s URL is in PNG format. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="user_id">The ID of the user for whom to return results for. </param>
		/// <param name="screen_name">The screen name of the user for whom to return results for. </param>
        [HttpGet]
        [Route("profile_banner{mediaTypeExtension}")]
        public virtual IHttpActionResult GetProfileBannerByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null)
        {
            return GetProfileBannerByMediaTypeExtension(mediaTypeExtension,user_id,screen_name);
        }


        /// <summary>
		/// Access to Twitter&apos;s suggested user list. This returns the list of suggested  user categories. The category can be used in GET users/suggestions/{slug}.json  to get the users in that category. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="lang">Restricts the suggested categories to the requested language. The language  must be specified by the appropriate two letter ISO 639-1 representation.  Currently supported languages are provided by the GET help/languages API  request. Unsupported language codes will receive English (en) results. If  you use lang in this request, ensure you also include it when requesting  the GET users/suggestions/{slug}.json list. </param>
        [HttpGet]
        [Route("suggestions{mediaTypeExtension}")]
        public virtual IHttpActionResult GetSuggestionsByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] string lang = null)
        {
            return GetSuggestionsByMediaTypeExtension(mediaTypeExtension,lang);
        }


        /// <summary>
		/// Access the users in a given category of the Twitter suggested user list. It is recommended that applications cache this data for no more than one hour. 
		/// </summary>
		/// <param name="slug">The short name of list or a category</param>
		/// <param name="mediaTypeExtension"></param>
		/// <param name="lang">Restricts the suggested categories to the requested language. The  language must be specified by the appropriate two letter ISO 639-1  representation. Currently supported languages are provided by the GET  help/languages API request. Unsupported language codes will receive  English (en) results. If you use lang in this request, ensure you also  include it when requesting the GET users/suggestions/:slug list. </param>
        [HttpGet]
        [Route("suggestions/{slug}/{mediaTypeExtension}")]
        public virtual IHttpActionResult GetBySlugMediaTypeExtensionBase([FromUri] string slug,[FromUri] string mediaTypeExtension,[FromUri] string lang = null)
        {
            return GetBySlugMediaTypeExtension(slug,mediaTypeExtension,lang);
        }


        /// <summary>
		/// Access the users in a given category of the Twitter suggested user list  and return their most recent status if they are not a protected user. 
		/// </summary>
		/// <param name="mediaTypeExtension"></param>
		/// <param name="slug"></param>
        [HttpGet]
        [Route("suggestions/{slug}/{mediaTypeExtension}/members{mediaTypeExtension}")]
        public virtual IHttpActionResult GetMembersByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] string slug)
        {
            return GetMembersByMediaTypeExtension(mediaTypeExtension,slug);
        }


        /// <summary>
		/// Report the specified user as a spam account to Twitter. Additionally performs  the equivalent of POST blocks/create on behalf of the authenticated user. One of parameters must be provided. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpPost]
        [Route("report_spam{mediaTypeExtension}")]
        public virtual IHttpActionResult PostBase(string json,[FromUri] string mediaTypeExtension)
        {
            return Post(json,mediaTypeExtension);
        }

    }

    public partial class FavoritesController : ApiController
    {


        /// <summary>
		/// Returns the 20 most recent Tweets favorited by the authenticating or specified  user. If you do not provide either a user_id or screen_name to this method, it  will assume you are requesting on behalf of the authenticating user. Specify  one or the other for best results. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="user_id">The ID of the user for whom to return results for.</param>
		/// <param name="screen_name">The screen name of the user for whom to return results for.</param>
		/// <param name="count">Specifies the number of records to retrieve. Must be less than or equal  to 200. Defaults to 20. </param>
		/// <param name="since_id">Returns results with an ID greater than (that is, more recent than) the  specified ID. There are limits to the number of Tweets which can be  accessed through the API. If the limit of Tweets has occured since the  since_id, the since_id will be forced to the oldest ID available. </param>
		/// <param name="max_id">Returns results with an ID less than (that is, older than) or equal to  the specified ID. </param>
		/// <param name="include_entities">The entities node will be omitted when set to false.</param>
		/// <returns>ListMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(ListMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("list{mediaTypeExtension}")]
        public virtual IHttpActionResult GetBase([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? count = null,[FromUri] int? since_id = null,[FromUri] int? max_id = null,[FromUri] string include_entities = null)
        {
            return Get(mediaTypeExtension,user_id,screen_name,count,since_id,max_id,include_entities);
        }


        /// <summary>
		/// Un-favorites the status specified in the ID parameter as the authenticating  user. Returns the un-favorited status in the requested format when successful. This process invoked by this method is asynchronous. The immediately returned  status may not indicate the resultant favorited status of the tweet. A 200 OK  response from this method will indicate whether the intended action was  successful or not. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>DestroyMediaTypeExtensionPostOKResponseContent</returns>
        [ResponseType(typeof(DestroyMediaTypeExtensionPostOKResponseContent))]
        [HttpPost]
        [Route("destroy{mediaTypeExtension}")]
        public virtual IHttpActionResult PostBase(string json,[FromUri] string mediaTypeExtension)
        {
            return Post(json,mediaTypeExtension);
        }


        /// <summary>
		/// Favorites the status specified in the ID parameter as the authenticating user.  Returns the favorite status when successful. This process invoked by this method is asynchronous. The immediately returned  status may not indicate the resultant favorited status of the tweet. A 200 OK  response from this method will indicate whether the intended action was  successful or not. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>CreateMediaTypeExtensionPostOKResponseContent</returns>
        [ResponseType(typeof(CreateMediaTypeExtensionPostOKResponseContent))]
        [HttpPost]
        [Route("create{mediaTypeExtension}")]
        public virtual IHttpActionResult PostCreateByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostCreateByMediaTypeExtension(json,mediaTypeExtension);
        }

    }

    public partial class ListsController : ApiController
    {


        /// <summary>
		/// Returns all lists the authenticating or specified user subscribes to, including  their own. The user is specified using the user_id or screen_name parameters.  If no user is given, the authenticating user is used. This method used to be GET lists in version 1.0 of the API and has been renamed  for consistency with other call. A maximum of 100 results will be returned by this call. Subscribed lists are  returned first, followed by owned lists. This means that if a user subscribes  to 90 lists and owns 20 lists, this method returns 90 subscriptions and 10 owned  lists. The reverse method returns owned lists first, so with reverse=true, 20  owned lists and 80 subscriptions would be returned. If your goal is to obtain  every list a user owns or subscribes to, use GET lists/ownerships and/or  GET lists/subscriptions instead. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="user_id">The ID of the user for whom to return results for. Helpful for  disambiguating when a valid user ID is also a valid screen name. </param>
		/// <param name="screen_name">The screen name of the user for whom to return results for. Helpful for  disambiguating when a valid screen name is also a user ID. </param>
		/// <param name="reverse">Set this to true if you would like owned lists to be returned first. See  description above for information on how this parameter works. </param>
		/// <returns>ListMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(ListMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("list{mediaTypeExtension}")]
        public virtual IHttpActionResult GetBase([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] string reverse = null)
        {
            return Get(mediaTypeExtension,user_id,screen_name,reverse);
        }


        /// <summary>
		/// Returns a timeline of tweets authored by members of the specified list.  Retweets are included by default. Use the include_rts=false parameter to  omit retweets. Embedded Timelines is a great way to embed list timelines on your website. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="list_id">The numerical id of the list. </param>
		/// <param name="slug">You can identify a list by its slug instead of its numerical id. If you  decide to do so, note that you&apos;ll also have to specify the list owner  using the owner_id or owner_screen_name parameters. </param>
		/// <param name="owner_screen_name">The screen name of the user who owns the list being requested by a slug. </param>
		/// <param name="owner_id">The user ID of the user who owns the list being requested by a slug. </param>
		/// <param name="since_id">Returns results with an ID greater than (that is, more recent than) the  specified ID. There are limits to the number of Tweets which can be  accessed through the API. If the limit of Tweets has occured since the  since_id, the since_id will be forced to the oldest ID available. </param>
		/// <param name="max_id">Returns results with an ID less than (that is, older than) or equal to  the specified ID. </param>
		/// <param name="count">Specifies the number of results to retrieve per &quot;page.&quot; </param>
		/// <param name="include_rts">When set to either true, t or 1, the list timeline will contain native  retweets (if they exist) in addition to the standard stream of tweets.  The output format of retweeted tweets is identical to the representation  you see in home_timeline. </param>
        [HttpGet]
        [Route("statuses{mediaTypeExtension}")]
        public virtual IHttpActionResult GetStatusesByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int list_id,[FromUri] string slug,[FromUri] string include_entities = null,[FromUri] string owner_screen_name = null,[FromUri] int? owner_id = null,[FromUri] int? since_id = null,[FromUri] int? max_id = null,[FromUri] int? count = null,[FromUri] string include_rts = null)
        {
            return GetStatusesByMediaTypeExtension(mediaTypeExtension,list_id,slug,include_entities,owner_screen_name,owner_id,since_id,max_id,count,include_rts);
        }


        /// <summary>
		/// Returns the lists the specified user has been added to. If user_id or  screen_name are not provided the memberships for the authenticating user  are returned. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="user_id">The ID of the user for whom to return results for. Helpful for  disambiguating when a valid user ID is also a valid screen name. </param>
		/// <param name="screen_name">The screen name of the user for whom to return results for. Helpful  for disambiguating when a valid screen name is also a user ID. </param>
		/// <param name="cursor">Breaks the results into pages. Provide a value of -1 to begin paging.  Provide values as returned in the response body&apos;s next_cursor and  previous_cursor attributes to page back and forth in the list. It is  recommended to always use cursors when the method supports them. See  Using cursors to navigate collections for more information. </param>
		/// <param name="filter_to_owned_lists">When set to true, t or 1, will return just lists the authenticating  user owns, and the user represented by user_id or screen_name is a  member of. </param>
        [HttpGet]
        [Route("memberships{mediaTypeExtension}")]
        public virtual IHttpActionResult GetMembershipsByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? cursor = null,[FromUri] string filter_to_owned_lists = null)
        {
            return GetMembershipsByMediaTypeExtension(mediaTypeExtension,user_id,screen_name,cursor,filter_to_owned_lists);
        }


        /// <summary>
		/// Returns the subscribers of the specified list. Private list subscribers will  only be shown if the authenticated user owns the specified list. Either a list_id or a slug is required. If providing a list_slug, an  owner_screen_name or owner_id is also required. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="list_id">The numerical id of the list.</param>
		/// <param name="slug">You can identify a list by its slug instead of its numerical id. If you  decide to do so, note that you&apos;ll also have to specify the list owner  using the owner_id or owner_screen_name parameters. </param>
		/// <param name="owner_screen_name">The screen name of the user who owns the list being requested by a slug. </param>
		/// <param name="owner_id">The user ID of the user who owns the list being requested by a slug. </param>
		/// <param name="cursor">Breaks the results into pages. A single page contains 20 lists. Provide  a value of -1 to begin paging. Provide values as returned in the response  body&apos;s next_cursor and previous_cursor attributes to page back and forth  in the list. </param>
		/// <param name="skip_status">When set to either true, t or 1 statuses will not be included in the  returned user objects. </param>
        [HttpGet]
        [Route("subscribers{mediaTypeExtension}")]
        public virtual IHttpActionResult GetSubscribersByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int list_id,[FromUri] string slug,[FromUri] string include_entities = null,[FromUri] string owner_screen_name = null,[FromUri] int? owner_id = null,[FromUri] int? cursor = null,[FromUri] string skip_status = null)
        {
            return GetSubscribersByMediaTypeExtension(mediaTypeExtension,list_id,slug,include_entities,owner_screen_name,owner_id,cursor,skip_status);
        }


        /// <summary>
		/// Subscribes the authenticated user to the specified list. Either a list_id or a slug is required. If providing a list_slug, an  owner_screen_name or owner_id is also required. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension"></param>
        [HttpPost]
        [Route("subscribers{mediaTypeExtension}/create")]
        public virtual IHttpActionResult PostBase(string json,[FromUri] string mediaTypeExtension)
        {
            return Post(json,mediaTypeExtension);
        }


        /// <summary>
		/// Check if the specified user is a subscriber of the specified list. Returns  the user if they are subscriber. Either a list_id or a slug is required. If providing a list_slug, an  owner_screen_name or owner_id is also required. 
		/// </summary>
		/// <param name="mediaTypeExtension"></param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="owner_screen_name">The screen name of the user who owns the list being requested by a slug. </param>
		/// <param name="owner_id">The user ID of the user who owns the list being requested by a slug. </param>
		/// <param name="list_id">The numerical id of the list.</param>
		/// <param name="slug">You can identify a list by its slug instead of its numerical id. If  you decide to do so, note that you&apos;ll also have to specify the list  owner using the owner_id or owner_screen_name parameters. </param>
		/// <param name="user_id">The ID of the user for whom to return results for. Helpful for  disambiguating when a valid user ID is also a valid screen name. </param>
		/// <param name="screen_name">The screen name of the user for whom to return results for. Helpful  for disambiguating when a valid screen name is also a user ID. </param>
        [HttpGet]
        [Route("subscribers{mediaTypeExtension}/show")]
        public virtual IHttpActionResult GetShowBase([FromUri] string mediaTypeExtension,[FromUri] int list_id,[FromUri] string slug,[FromUri] int user_id,[FromUri] string screen_name,[FromUri] string include_entities = null,[FromUri] string owner_screen_name = null,[FromUri] int? owner_id = null)
        {
            return GetShow(mediaTypeExtension,list_id,slug,user_id,screen_name,include_entities,owner_screen_name,owner_id);
        }


        /// <summary>
		/// Unsubscribes the authenticated user from the specified list. Either a list_id or a slug is required. If providing a list_slug, an  owner_screen_name or owner_id is also required. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension"></param>
        [HttpPost]
        [Route("subscribers{mediaTypeExtension}/destroy")]
        public virtual IHttpActionResult PostDestroyBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostDestroy(json,mediaTypeExtension);
        }


        /// <summary>
		/// Returns the members of the specified list. Private list members will only be  shown if the authenticated user owns the specified list. Either a list_id or a slug is required. If providing a list_slug, an  owner_screen_name or owner_id is also required. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="list_id">The numerical id of the list.</param>
		/// <param name="slug">You can identify a list by its slug instead of its numerical id. If you  decide to do so, note that you&apos;ll also have to specify the list owner  using the owner_id or owner_screen_name parameters. </param>
		/// <param name="owner_screen_name">The screen name of the user who owns the list being requested by a slug. </param>
		/// <param name="owner_id">The user ID of the user who owns the list being requested by a slug. </param>
		/// <param name="cursor">Causes the collection of list members to be broken into &quot;pages&quot; of  somewhat consistent size. If no cursor is provided, a value of -1 will  be assumed, which is the first &quot;page.&quot; The response from the API will include a previous_cursor and next_cursor  to allow paging back and forth. See Using cursors to navigate collections  for more information. </param>
        [HttpGet]
        [Route("members{mediaTypeExtension}")]
        public virtual IHttpActionResult GetMembersByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int list_id,[FromUri] string slug,[FromUri] string include_entities = null,[FromUri] string owner_screen_name = null,[FromUri] int? owner_id = null,[FromUri] int? cursor = null)
        {
            return GetMembersByMediaTypeExtension(mediaTypeExtension,list_id,slug,include_entities,owner_screen_name,owner_id,cursor);
        }


        /// <summary>
		/// Removes the specified member from the list. The authenticated user must be  the list&apos;s owner to remove members from the list. Either a list_id or a slug is required. If providing a list_slug, an  owner_screen_name or owner_id is also required. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpPost]
        [Route("members/destroy{mediaTypeExtension}")]
        public virtual IHttpActionResult PostDestroyByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostDestroyByMediaTypeExtension(json,mediaTypeExtension);
        }


        /// <summary>
		/// Adds multiple members to a list, by specifying a comma-separated list of  member ids or screen names. The authenticated user must own the list to be  able to add members to it. Note that lists can&apos;t have more than 5,000 members,  and you are limited to adding up to 100 members to a list at a time with this  method. Please note that there can be issues with lists that rapidly remove and add  memberships. Take care when using these methods such that you are not too  rapidly switching between removals and adds on the same list. Either a list_id or a slug is required. If providing a list_slug, an  owner_screen_name or owner_id is also required. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpPost]
        [Route("members/create_all{mediaTypeExtension}")]
        public virtual IHttpActionResult PostCreateAllByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostCreateAllByMediaTypeExtension(json,mediaTypeExtension);
        }


        /// <summary>
		/// Check if the specified user is a member of the specified list. Either a list_id or a slug is required. If providing a list_slug, an  owner_screen_name or owner_id is also required. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="include_entities">The entities node will not be included when set to false.</param>
		/// <param name="list_id">The numerical id of the list.</param>
		/// <param name="slug">You can identify a list by its slug instead of its numerical id. If you  decide to do so, note that you&apos;ll also have to specify the list owner  using the owner_id or owner_screen_name parameters. </param>
		/// <param name="user_id">The ID of the user for whom to return results for. Helpful for  disambiguating when a valid user ID is also a valid screen name. </param>
		/// <param name="screen_name">The screen name of the user for whom to return results for. Helpful for  disambiguating when a valid screen name is also a user ID. </param>
		/// <param name="owner_screen_name">The screen name of the user who owns the list being requested by a slug. </param>
		/// <param name="owner_id">The user ID of the user who owns the list being requested by a slug. </param>
		/// <param name="skip_status">When set to either true, t or 1 statuses will not be included in the  returned user objects. </param>
		/// <returns>ShowMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(ShowMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("members/show{mediaTypeExtension}")]
        public virtual IHttpActionResult GetShowByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int list_id,[FromUri] string slug,[FromUri] int user_id,[FromUri] string screen_name,[FromUri] string include_entities = null,[FromUri] string owner_screen_name = null,[FromUri] int? owner_id = null,[FromUri] string skip_status = null)
        {
            return GetShowByMediaTypeExtension(mediaTypeExtension,list_id,slug,user_id,screen_name,include_entities,owner_screen_name,owner_id,skip_status);
        }


        /// <summary>
		/// Add a member to a list. The authenticated user must own the list to be able  to add members to it. Note that lists can&apos;t have more than 500 members. Either a list_id or a slug is required. If providing a list_slug, an  owner_screen_name or owner_id is also required. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpPost]
        [Route("members{mediaTypeExtension}/create")]
        public virtual IHttpActionResult PostCreateBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostCreate(json,mediaTypeExtension);
        }


        /// <summary>
		/// Deletes the specified list. The authenticated user must own the list to be  able to destroy it. Either a list_id or a slug is required. If providing a list_slug, an  owner_screen_name or owner_id is also required. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>DestroyMediaTypeExtensionPostOKResponseContent</returns>
        [ResponseType(typeof(DestroyMediaTypeExtensionPostOKResponseContent))]
        [HttpPost]
        [Route("destroy{mediaTypeExtension}")]
        public virtual IHttpActionResult PostABase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostA(json,mediaTypeExtension);
        }


        /// <summary>
		/// Updates the specified list. The authenticated user must own the list to be  able to update it. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpPost]
        [Route("update{mediaTypeExtension}")]
        public virtual IHttpActionResult PostUpdateByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostUpdateByMediaTypeExtension(json,mediaTypeExtension);
        }


        /// <summary>
		/// Creates a new list for the authenticated user. Note that you can&apos;t create  more than 20 lists per account. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>CreateMediaTypeExtensionPostOKResponseContent</returns>
        [ResponseType(typeof(CreateMediaTypeExtensionPostOKResponseContent))]
        [HttpPost]
        [Route("create{mediaTypeExtension}")]
        public virtual IHttpActionResult PostCreateByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostCreateByMediaTypeExtension(json,mediaTypeExtension);
        }


        /// <summary>
		/// Returns the specified list. Private lists will only be shown if the  authenticated user owns the specified list. Either a list_id or a slug is required. If providing a list_slug, an  owner_screen_name or owner_id is also required. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="list_id">The numerical id of the list.</param>
		/// <param name="slug">You can identify a list by its slug instead of its numerical id. If you  decide to do so, note that you&apos;ll also have to specify the list owner  using the owner_id or owner_screen_name parameters. </param>
		/// <param name="owner_screen_name">The screen name of the user who owns the list being requested by a slug. </param>
		/// <param name="owner_id">The user ID of the user who owns the list being requested by a slug. </param>
		/// <returns>ShowMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(ShowMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("show{mediaTypeExtension}")]
        public virtual IHttpActionResult GetABase([FromUri] string mediaTypeExtension,[FromUri] int list_id,[FromUri] string slug,[FromUri] string owner_screen_name = null,[FromUri] int? owner_id = null)
        {
            return GetA(mediaTypeExtension,list_id,slug,owner_screen_name,owner_id);
        }


        /// <summary>
		/// Obtain a collection of the lists the specified user is subscribed to, 20  lists per page by default. Does not include the user&apos;s own lists. A user_id or screen_name must be provided. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="user_id">The ID of the user for whom to return results for. Helpful for  disambiguating when a valid user ID is also a valid screen name. </param>
		/// <param name="screen_name">The screen name of the user for whom to return results for. Helpful  for disambiguating when a valid screen name is also a user ID. </param>
		/// <param name="count">The amount of results to return per page. Defaults to 20. No more than  1000 results will ever be returned in a single page. </param>
		/// <param name="cursor">Breaks the results into pages. Provide a value of -1 to begin paging.  Provide values as returned in the response body&apos;s next_cursor and  previous_cursor attributes to page back and forth in the list. It is  recommended to always use cursors when the method supports them. See  Using cursors to navigate collections for more information. </param>
        [HttpGet]
        [Route("subscriptions{mediaTypeExtension}")]
        public virtual IHttpActionResult GetSubscriptionsByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? count = null,[FromUri] int? cursor = null)
        {
            return GetSubscriptionsByMediaTypeExtension(mediaTypeExtension,user_id,screen_name,count,cursor);
        }


        /// <summary>
		/// Removes multiple members from a list, by specifying a comma-separated list  of member ids or screen names. The authenticated user must own the list to  be able to remove members from it. Note that lists can&apos;t have more than 500  members, and you are limited to removing up to 100 members to a list at a  time with this method. Please note that there can be issues with lists that rapidly remove and add  memberships. Take care when using these methods such that you are not too  rapidly switching between removals and adds on the same list. Either a list_id or a slug is required. If providing a list_slug, an  owner_screen_name or owner_id is also required. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpPost]
        [Route("members/destroy_all{mediaTypeExtension}")]
        public virtual IHttpActionResult PostMembersDestroyAllByMediaTypeExtensionBase(string json,[FromUri] string mediaTypeExtension)
        {
            return PostMembersDestroyAllByMediaTypeExtension(json,mediaTypeExtension);
        }


        /// <summary>
		/// Returns the lists owned by the specified Twitter user. Private lists will  only be shown if the authenticated user is also the owner of the lists. A user_id or screen_name must be provided. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="user_id">The ID of the user for whom to return results for.</param>
		/// <param name="screen_name">The screen name of the user for whom to return results for.</param>
		/// <param name="count">The amount of results to return per page. Defaults to 20. No more than  1000 results will ever be returned in a single page. </param>
		/// <param name="cursor">Breaks the results into pages. Provide a value of -1 to begin paging.  Provide values as returned in the response body&apos;s next_cursor and  previous_cursor attributes to page back and forth in the list. It is  recommended to always use cursors when the method supports them. See  Using cursors to navigate collections for more information. </param>
        [HttpGet]
        [Route("ownerships{mediaTypeExtension}")]
        public virtual IHttpActionResult GetOwnershipsByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? count = null,[FromUri] string cursor = null)
        {
            return GetOwnershipsByMediaTypeExtension(mediaTypeExtension,user_id,screen_name,count,cursor);
        }

    }

    public partial class SavedSearchesController : ApiController
    {


        /// <summary>
		/// Returns the authenticated user&apos;s saved search queries. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>ListMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(ListMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("list{mediaTypeExtension}")]
        public virtual IHttpActionResult GetBase([FromUri] string mediaTypeExtension)
        {
            return Get(mediaTypeExtension);
        }


        /// <summary>
		/// Retrieve the information for the saved search represented by the given id.  The authenticating user must be the owner of saved search ID being requested. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="id">The ID of the saved search.</param>
		/// <returns>ShowIdGetOKResponseContent</returns>
        [ResponseType(typeof(ShowIdGetOKResponseContent))]
        [HttpGet]
        [Route("show/{id}")]
        public virtual IHttpActionResult GetShowByIdBase([FromUri] string mediaTypeExtension,[FromUri] int id)
        {
            return GetShowById(mediaTypeExtension,id);
        }


        /// <summary>
		/// Create a new saved search for the authenticated user. A user may only have  25 saved searches. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>CreateMediaTypeExtensionPostOKResponseContent</returns>
        [ResponseType(typeof(CreateMediaTypeExtensionPostOKResponseContent))]
        [HttpPost]
        [Route("create{mediaTypeExtension}")]
        public virtual IHttpActionResult PostBase(string json,[FromUri] string mediaTypeExtension)
        {
            return Post(json,mediaTypeExtension);
        }


        /// <summary>
		/// Destroys a saved search for the authenticating user. The authenticating  user must be the owner of saved search id being destroyed. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="id">The ID of the saved search.</param>
		/// <returns>DestroyIdPostOKResponseContent</returns>
        [ResponseType(typeof(DestroyIdPostOKResponseContent))]
        [HttpPost]
        [Route("destroy/{id}")]
        public virtual IHttpActionResult PostDestroyByIdBase(string json,[FromUri] string mediaTypeExtension,[FromUri] int id)
        {
            return PostDestroyById(json,mediaTypeExtension,id);
        }

    }

    public partial class GeoController : ApiController
    {


        /// <summary>
		/// Returns all the information about a known place.
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="place_id">A place in the world. These IDs can be retrieved from geo/reverse_geocode.</param>
		/// <returns>IdPlaceIdMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(IdPlaceIdMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("id/{place_id}/{mediaTypeExtension}")]
        public virtual IHttpActionResult GetBase([FromUri] string mediaTypeExtension,[FromUri] string place_id)
        {
            return Get(mediaTypeExtension,place_id);
        }


        /// <summary>
		/// Given a latitude and a longitude, searches for up to 20 places that can be  used as a place_id when updating a status. This request is an informative call and will deliver generalized results  about geography. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="lat">The latitude to search around. This parameter will be ignored unless it  is inside the range -90.0 to +90.0 (North is positive) inclusive. It  will also be ignored if there isn&apos;t a corresponding long parameter. </param>
		/// <param name="iplong">The longitude to search around. The valid ranges for longitude is -180.0  to +180.0 (East is positive) inclusive. This parameter will be ignored  if outside that range, if it is not a number, if geo_enabled is disabled,  or if there not a corresponding lat parameter. </param>
		/// <param name="accuracy">A hint on the &quot;region&quot; in which to search. If a number, then this is a  radius in meters, but it can also take a string that is suffixed with  ft to specify feet. If this is not passed in, then it is assumed to be  0m. If coming from a device, in practice, this value is whatever accuracy  the device has measuring its location (whether it be coming from a GPS,  WiFi triangulation, etc.). </param>
		/// <param name="granularity">This is the minimal granularity of place types to return and must be one  of: poi, neighborhood, city, admin or country. If no granularity is  provided for the request neighborhood is assumed. Setting this to city, for example, will find places which have a type of  city, admin or country. </param>
		/// <param name="max_results">A hint as to the number of results to return. This does not guarantee  that the number of results returned will equal max_results, but instead  informs how many &quot;nearby&quot; results to return. Ideally, only pass in the  number of places you intend to display to the user here. </param>
		/// <param name="callback">If supplied, the response will use the JSONP format with a callback of  the given name. </param>
		/// <returns>ReverseGeocodeMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(ReverseGeocodeMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("reverse_code{mediaTypeExtension}")]
        public virtual IHttpActionResult GetReverseGeocodeByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] decimal lat,[FromUri] decimal iplong,[FromUri] string accuracy = null,[FromUri] string granularity = null,[FromUri] int? max_results = null,[FromUri] string callback = null)
        {
            return GetReverseGeocodeByMediaTypeExtension(mediaTypeExtension,lat,iplong,accuracy,granularity,max_results,callback);
        }


        /// <summary>
		/// Search for places that can be attached to a statuses/update. Given a latitude  and a longitude pair, an IP address, or a name, this request will return a  list of all the valid places that can be used as the place_id when updating  a status. Conceptually, a query can be made from the user&apos;s location, retrieve a list  of places, have the user validate the location he or she is at, and then  send the ID of this location with a call to POST statuses/update. This is the recommended method to use find places that can be attached to  statuses/update. Unlike GET geo/reverse_geocode which provides raw data  access, this endpoint can potentially re-order places with regards to the  user who is authenticated. This approach is also preferred for interactive  place matching with the user. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="lat">The latitude to search around. This parameter will be ignored unless it  is inside the range -90.0 to +90.0 (North is positive) inclusive. It  will also be ignored if there isn&apos;t a corresponding long parameter. </param>
		/// <param name="iplong">The longitude to search around. The valid ranges for longitude is -180.0  to +180.0 (East is positive) inclusive. This parameter will be ignored  if outside that range, if it is not a number, if geo_enabled is disabled,  or if there not a corresponding lat parameter. </param>
		/// <param name="query">Free-form text to match against while executing a geo-based query, best  suited for finding nearby locations by name. Remember to URL encode the query. </param>
		/// <param name="ip">An IP address. Used when attempting to fix geolocation based off of the  user&apos;s IP address. </param>
		/// <param name="granularity">This is the minimal granularity of place types to return and must be one  of: poi, neighborhood, city, admin or country. If no granularity is  provided for the request neighborhood is assumed. Setting this to city, for example, will find places which have a type of  city, admin or country. </param>
		/// <param name="accuracy">A hint on the &quot;region&quot; in which to search. If a number, then this is a  radius in meters, but it can also take a string that is suffixed with  ft to specify feet. If this is not passed in, then it is assumed to be  0m. If coming from a device, in practice, this value is whatever accuracy  the device has measuring its location (whether it be coming from a GPS,  WiFi triangulation, etc.). </param>
		/// <param name="max_results">A hint as to the number of results to return. This does not guarantee  that the number of results returned will equal max_results, but instead  informs how many &quot;nearby&quot; results to return. Ideally, only pass in the  number of places you intend to display to the user here. </param>
		/// <param name="contained_within">This is the place_id which you would like to restrict the search results  to. Setting this value means only places within the given place_id will  be found. Specify a place_id. For example, to scope all results to places within  &quot;San Francisco, CA USA&quot;, you would specify a place_id of &quot;5a110d312052166f&quot; </param>
		/// <param name="attributestreet_address">This parameter searches for places which have this given street address.  There are other well-known, and application specific attributes available.  Custom attributes are also permitted. Learn more about Place Attributes. </param>
		/// <param name="callback">If supplied, the response will use the JSONP format with a callback of  the given name. </param>
		/// <returns>SearchMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(SearchMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("search{mediaTypeExtension}")]
        public virtual IHttpActionResult GetSearchByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] decimal? lat = null,[FromUri] decimal? iplong = null,[FromUri] string query = null,[FromUri] string ip = null,[FromUri] string granularity = null,[FromUri] string accuracy = null,[FromUri] int? max_results = null,[FromUri] string contained_within = null,[FromUri] string attributestreet_address = null,[FromUri] string callback = null)
        {
            return GetSearchByMediaTypeExtension(mediaTypeExtension,lat,iplong,query,ip,granularity,accuracy,max_results,contained_within,attributestreet_address,callback);
        }


        /// <summary>
		/// Locates places near the given coordinates which are similar in name. Conceptually you would use this method to get a list of known places to choose from first. Then, if the desired place doesn&apos;t exist, make a request to POST geo/place to create a new one. The token contained in the response is the token needed to be able to create a new place. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="lat">The latitude to search around. This parameter will be ignored unless it  is inside the range -90.0 to +90.0 (North is positive) inclusive. It  will also be ignored if there isn&apos;t a corresponding long parameter. </param>
		/// <param name="iplong">The longitude to search around. The valid ranges for longitude is -180.0  to +180.0 (East is positive) inclusive. This parameter will be ignored  if outside that range, if it is not a number, if geo_enabled is disabled,  or if there not a corresponding lat parameter. </param>
		/// <param name="name">The name a place is known as.</param>
		/// <param name="contained_within">This is the place_id which you would like to restrict the search results  to. Setting this value means only places within the given place_id will  be found. Specify a place_id. For example, to scope all results to places within  &quot;San Francisco, CA USA&quot;, you would specify a place_id of &quot;5a110d312052166f&quot; </param>
		/// <param name="attributestreet_address">This parameter searches for places which have this given street address.  There are other well-known, and application specific attributes available.  Custom attributes are also permitted. Learn more about Place Attributes. </param>
		/// <param name="callback">If supplied, the response will use the JSONP format with a callback of  the given name. </param>
		/// <returns>SimilarPlacesMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(SimilarPlacesMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("similar_places{mediaTypeExtension}")]
        public virtual IHttpActionResult GetSimilarPlacesByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] decimal lat,[FromUri] decimal iplong,[FromUri] string name,[FromUri] string contained_within = null,[FromUri] string attributestreet_address = null,[FromUri] string callback = null)
        {
            return GetSimilarPlacesByMediaTypeExtension(mediaTypeExtension,lat,iplong,name,contained_within,attributestreet_address,callback);
        }


        /// <summary>
		/// Creates a new place object at the given latitude and longitude. Before creating a place you need to query GET geo/similar_places with the  latitude, longitude and name of the place you wish to create. The query  will return an array of places which are similar to the one you wish to  create, and a token. If the place you wish to create isn&apos;t in the returned  array you can use the token with this method to create a new one. 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>PlaceMediaTypeExtensionPostOKResponseContent</returns>
        [ResponseType(typeof(PlaceMediaTypeExtensionPostOKResponseContent))]
        [HttpPost]
        [Route("place{mediaTypeExtension}")]
        public virtual IHttpActionResult PostBase(string json,[FromUri] string mediaTypeExtension)
        {
            return Post(json,mediaTypeExtension);
        }

    }

    public partial class TrendsController : ApiController
    {


        /// <summary>
		/// Returns the top 10 trending topics for a specific WOEID, if trending information  is available for it. The response is an array of &quot;trend&quot; objects that encode the name of the  trending topic, the query parameter that can be used to search for the topic  on Twitter Search, and the Twitter Search URL. This information is cached for 5 minutes. Requesting more frequently than  that will not return any more data, and will count against your rate limit usage. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="id">The Yahoo! Where On Earth ID of the location to return trending information  for. Global information is available by using 1 as the WOEID. </param>
		/// <param name="exclude">Setting this equal to hashtags will remove all hashtags from the trends list. </param>
		/// <returns>PlaceMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(PlaceMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("place{mediaTypeExtension}")]
        public virtual IHttpActionResult GetBase([FromUri] string mediaTypeExtension,[FromUri] int id,[FromUri] string exclude = null)
        {
            return Get(mediaTypeExtension,id,exclude);
        }


        /// <summary>
		/// Returns the locations that Twitter has trending topic information for. The response is an array of &quot;locations&quot; that encode the location&apos;s WOEID  and some other human-readable information such as a canonical name and  country the location belongs in. A WOEID is a Yahoo! Where On Earth ID. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>AvailableMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(AvailableMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("available{mediaTypeExtension}")]
        public virtual IHttpActionResult GetAvailableByMediaTypeExtensionBase([FromUri] string mediaTypeExtension)
        {
            return GetAvailableByMediaTypeExtension(mediaTypeExtension);
        }


        /// <summary>
		/// Returns the locations that Twitter has trending topic information for,  closest to a specified location. The response is an array of &quot;locations&quot; that encode the location&apos;s WOEID  and some other human-readable information such as a canonical name and  country the location belongs in. A WOEID is a Yahoo! Where On Earth ID. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="lat">The latitude to search around. This parameter will be ignored unless it  is inside the range -90.0 to +90.0 (North is positive) inclusive. It  will also be ignored if there isn&apos;t a corresponding long parameter. </param>
		/// <param name="iplong">The longitude to search around. The valid ranges for longitude is -180.0  to +180.0 (East is positive) inclusive. This parameter will be ignored  if outside that range, if it is not a number, if geo_enabled is disabled,  or if there not a corresponding lat parameter. </param>
		/// <returns>ClosestMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(ClosestMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("closest{mediaTypeExtension}")]
        public virtual IHttpActionResult GetClosestByMediaTypeExtensionBase([FromUri] string mediaTypeExtension,[FromUri] decimal lat,[FromUri] decimal iplong)
        {
            return GetClosestByMediaTypeExtension(mediaTypeExtension,lat,iplong);
        }

    }

    public partial class HelpController : ApiController
    {


        /// <summary>
		/// Returns the current configuration used by Twitter including twitter.com  slugs which are not usernames, maximum photo resolutions, and t.co URL  lengths. It is recommended applications request this endpoint when they are loaded,  but no more than once a day. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpGet]
        [Route("configuration{mediaTypeExtension}")]
        public virtual IHttpActionResult GetBase([FromUri] string mediaTypeExtension)
        {
            return Get(mediaTypeExtension);
        }


        /// <summary>
		/// Returns the list of languages supported by Twitter along with their ISO 639-1  code. The ISO 639-1 code is the two letter value to use if you include lang  with any of your requests. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
        [HttpGet]
        [Route("languages{mediaTypeExtension}")]
        public virtual IHttpActionResult GetLanguagesByMediaTypeExtensionBase([FromUri] string mediaTypeExtension)
        {
            return GetLanguagesByMediaTypeExtension(mediaTypeExtension);
        }


        /// <summary>
		/// Returns Twitter&apos;s Privacy Policy.
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>PrivacyMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(PrivacyMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("privacy{mediaTypeExtension}")]
        public virtual IHttpActionResult GetPrivacyByMediaTypeExtensionBase([FromUri] string mediaTypeExtension)
        {
            return GetPrivacyByMediaTypeExtension(mediaTypeExtension);
        }


        /// <summary>
		/// Returns the Twitter Terms of Service in the requested format. These are not  the same as the Developer Rules of the Road. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <returns>TosMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(TosMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("tos{mediaTypeExtension}")]
        public virtual IHttpActionResult GetTosByMediaTypeExtensionBase([FromUri] string mediaTypeExtension)
        {
            return GetTosByMediaTypeExtension(mediaTypeExtension);
        }

    }

    public partial class ApplicationRateLimitStatusMediaTypeExtensionController : ApiController
    {


        /// <summary>
		/// Returns the current rate limits for methods belonging to the specified  resource families. Each 1.1 API resource belongs to a &quot;resource family&quot; which is indicated in  its method documentation. You can typically determine a method&apos;s resource  family from the first component of the path after the resource version. This method responds with a map of methods belonging to the families specified  by the resources parameter, the current remaining uses for each of those  resources within the current rate limiting window, and its expiration time  in epoch time. It also includes a rate_limit_context field that indicates  the current access token or application-only authentication context. You may also issue requests to this method without any parameters to receive  a map of all rate limited GET methods. If your application only uses a few  of methods, please explicitly provide a resources parameter with the specified  resource families you work with. When using app-only auth, this method&apos;s response indicates the app-only auth  rate limiting context. Read more about REST API Rate Limiting in v1.1 and review the limits. 
		/// </summary>
		/// <param name="mediaTypeExtension">Use .json to specify application/json media type.</param>
		/// <param name="resources">A comma-separated list of resource families you want to know the current rate limit disposition for. For best performance, only specify the resource families pertinent to your application. See Rate Limiting in API v1.1 for more information. </param>
		/// <returns>ApplicationRateLimitStatusMediaTypeExtensionGetOKResponseContent</returns>
        [ResponseType(typeof(ApplicationRateLimitStatusMediaTypeExtensionGetOKResponseContent))]
        [HttpGet]
        [Route("{mediaTypeExtension}")]
        public virtual IHttpActionResult GetBase([FromUri] string mediaTypeExtension,[FromUri] string resources = null)
        {
            return Get(mediaTypeExtension,resources);
        }

    }

}