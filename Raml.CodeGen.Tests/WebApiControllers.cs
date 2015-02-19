
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


        private IHttpActionResult Get([FromUri] string mediaTypeExtension,[FromUri] string trim_user = null,[FromUri] string include_entities = null,[FromUri] int? count = null,[FromUri] int? since_id = null,[FromUri] int? max_id = null,[FromUri] string contributor_details = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetUserTimelineByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] string trim_user = null,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? since_id = null,[FromUri] int? count = null,[FromUri] int? max_id = null,[FromUri] string contributor_details = null,[FromUri] string exclude_replies = null,[FromUri] string include_rts = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetHomeTimelineByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] string trim_user = null,[FromUri] string include_entities = null,[FromUri] int? count = null,[FromUri] int? since_id = null,[FromUri] int? max_id = null,[FromUri] string exclude_replies = null,[FromUri] string contributor_details = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetRetweetsOfMeByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] string trim_user = null,[FromUri] string include_entities = null,[FromUri] int? count = null,[FromUri] int? since_id = null,[FromUri] int? max_id = null,[FromUri] string include_user_entities = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetRetweetsByIdMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int id,[FromUri] string trim_user = null,[FromUri] int? count = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetShowByIdMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int id,[FromUri] string trim_user = null,[FromUri] string include_entities = null,[FromUri] string include_my_retweet = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult Post(string json,[FromUri] string mediaTypeExtension,[FromUri] int id)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostUpdateByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostRetweetByIdMediaTypeExtension(string json,[FromUri] string mediaTypeExtension,[FromUri] int id)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostUpdateWithMediaByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetOembedByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int id,[FromUri] string url,[FromUri] int? maxwidth = null,[FromUri] string hide_media = null,[FromUri] string hide_thread = null,[FromUri] string omit_script = null,[FromUri] string align = null,[FromUri] string related = null,[FromUri] string lang = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetRetweetersIdsByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int id,[FromUri] int? cursor = null,[FromUri] string stringify_ids = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostFilterByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetSampleByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] string delimited = null,[FromUri] string stall_warnings = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetFirehoseByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int? count = null,[FromUri] string delimited = null,[FromUri] string stall_warnings = null)
        {
            // put you code here
            return Ok();
        }

    }

    public partial class SearchTweetsMediaTypeExtensionController : ApiController
    {


        private IHttpActionResult Get([FromUri] string mediaTypeExtension,[FromUri] string q,[FromUri] string include_entities = null,[FromUri] string geocode = null,[FromUri] string lang = null,[FromUri] string locale = null,[FromUri] string result_type = null,[FromUri] int? count = null,[FromUri] string until = null,[FromUri] int? since_id = null,[FromUri] int? max_id = null,[FromUri] string callback = null)
        {
            // put you code here
            return Ok();
        }

    }

    public partial class DirectMessagesMediaTypeExtensionController : ApiController
    {


        private IHttpActionResult Get([FromUri] string mediaTypeExtension,[FromUri] string include_entities = null,[FromUri] int? since_id = null,[FromUri] int? max_id = null,[FromUri] int? count = null,[FromUri] string skip_status = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetSentByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] string include_entities = null,[FromUri] int? since_id = null,[FromUri] int? max_id = null,[FromUri] int? count = null,[FromUri] int? page = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetShowByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int id)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult Post(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostNewByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }

    }

    public partial class FriendshipsMediaTypeExtensionController : ApiController
    {


        private IHttpActionResult Get([FromUri] string mediaTypeExtension,[FromUri] string stringify_ids = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetLookupByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] string screen_name = null,[FromUri] string user_id = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetIncomingByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int? cursor = null,[FromUri] string stringify_ids = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetOutgoingByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int? cursor = null,[FromUri] string stringify_ids = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult Post(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostDestroyByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostUpdateByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetShowByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int? source_id = null,[FromUri] string source_screen_name = null,[FromUri] int? target_id = null,[FromUri] string target_screen_name = null)
        {
            // put you code here
            return Ok();
        }

    }

    public partial class FriendsController : ApiController
    {


        private IHttpActionResult Get([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? cursor = null,[FromUri] string stringify_ids = null,[FromUri] int? count = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetListByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? cursor = null,[FromUri] string skip_status = null,[FromUri] string include_user_entities = null)
        {
            // put you code here
            return Ok();
        }

    }

    public partial class FollowersController : ApiController
    {


        private IHttpActionResult Get([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? cursor = null,[FromUri] string stringify_ids = null,[FromUri] int? count = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetListByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? cursor = null,[FromUri] string skip_status = null,[FromUri] string include_user_entities = null)
        {
            // put you code here
            return Ok();
        }

    }

    public partial class AccountController : ApiController
    {


        private IHttpActionResult Get([FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult Post(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetVerifyCredentialsByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] string include_entities = null,[FromUri] string skip_status = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostUpdateDeliveryDeviceByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostUpdateProfileByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostUpdateProfileBackgroundImageByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostUpdateProfileColorsByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostUpdateProfileImageByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostUpdateProfileBannerByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostRemoveProfileBannerByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }

    }

    public partial class BlocksController : ApiController
    {


        private IHttpActionResult Get([FromUri] string mediaTypeExtension,[FromUri] string include_entities = null,[FromUri] string skip_status = null,[FromUri] int? cursor = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetIdsByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] string stringify_ids = null,[FromUri] int? cursor = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult Post(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostDestroyByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }

    }

    public partial class UsersController : ApiController
    {


        private IHttpActionResult Get([FromUri] string mediaTypeExtension,[FromUri] string include_entities = null,[FromUri] string screen_name = null,[FromUri] string user_id = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetShowByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int user_id,[FromUri] string include_entities = null,[FromUri] string screen_name = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetSearchByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] string q,[FromUri] string include_entities = null,[FromUri] string page = null,[FromUri] int? count = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetContributeesByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] string include_entities = null,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] string skip_status = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetContributorsByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] string include_entities = null,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] string skip_status = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetProfileBannerByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetSuggestionsByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] string lang = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetBySlugMediaTypeExtension([FromUri] string slug,[FromUri] string mediaTypeExtension,[FromUri] string lang = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetMembersByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] string slug)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult Post(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }

    }

    public partial class FavoritesController : ApiController
    {


        private IHttpActionResult Get([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? count = null,[FromUri] int? since_id = null,[FromUri] int? max_id = null,[FromUri] string include_entities = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult Post(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostCreateByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }

    }

    public partial class ListsController : ApiController
    {


        private IHttpActionResult Get([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] string reverse = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetStatusesByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int list_id,[FromUri] string slug,[FromUri] string include_entities = null,[FromUri] string owner_screen_name = null,[FromUri] int? owner_id = null,[FromUri] int? since_id = null,[FromUri] int? max_id = null,[FromUri] int? count = null,[FromUri] string include_rts = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetMembershipsByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? cursor = null,[FromUri] string filter_to_owned_lists = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetSubscribersByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int list_id,[FromUri] string slug,[FromUri] string include_entities = null,[FromUri] string owner_screen_name = null,[FromUri] int? owner_id = null,[FromUri] int? cursor = null,[FromUri] string skip_status = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult Post(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetShow([FromUri] string mediaTypeExtension,[FromUri] int list_id,[FromUri] string slug,[FromUri] int user_id,[FromUri] string screen_name,[FromUri] string include_entities = null,[FromUri] string owner_screen_name = null,[FromUri] int? owner_id = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostDestroy(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetMembersByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int list_id,[FromUri] string slug,[FromUri] string include_entities = null,[FromUri] string owner_screen_name = null,[FromUri] int? owner_id = null,[FromUri] int? cursor = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostDestroyByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostCreateAllByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetShowByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int list_id,[FromUri] string slug,[FromUri] int user_id,[FromUri] string screen_name,[FromUri] string include_entities = null,[FromUri] string owner_screen_name = null,[FromUri] int? owner_id = null,[FromUri] string skip_status = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostCreate(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostA(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostUpdateByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostCreateByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetA([FromUri] string mediaTypeExtension,[FromUri] int list_id,[FromUri] string slug,[FromUri] string owner_screen_name = null,[FromUri] int? owner_id = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetSubscriptionsByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? count = null,[FromUri] int? cursor = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostMembersDestroyAllByMediaTypeExtension(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetOwnershipsByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] int? user_id = null,[FromUri] string screen_name = null,[FromUri] int? count = null,[FromUri] string cursor = null)
        {
            // put you code here
            return Ok();
        }

    }

    public partial class SavedSearchesController : ApiController
    {


        private IHttpActionResult Get([FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetShowById([FromUri] string mediaTypeExtension,[FromUri] int id)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult Post(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult PostDestroyById(string json,[FromUri] string mediaTypeExtension,[FromUri] int id)
        {
            // put you code here
            return Ok();
        }

    }

    public partial class GeoController : ApiController
    {


        private IHttpActionResult Get([FromUri] string mediaTypeExtension,[FromUri] string place_id)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetReverseGeocodeByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] decimal lat,[FromUri] decimal iplong,[FromUri] string accuracy = null,[FromUri] string granularity = null,[FromUri] int? max_results = null,[FromUri] string callback = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetSearchByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] decimal? lat = null,[FromUri] decimal? iplong = null,[FromUri] string query = null,[FromUri] string ip = null,[FromUri] string granularity = null,[FromUri] string accuracy = null,[FromUri] int? max_results = null,[FromUri] string contained_within = null,[FromUri] string attributestreet_address = null,[FromUri] string callback = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetSimilarPlacesByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] decimal lat,[FromUri] decimal iplong,[FromUri] string name,[FromUri] string contained_within = null,[FromUri] string attributestreet_address = null,[FromUri] string callback = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult Post(string json,[FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }

    }

    public partial class TrendsController : ApiController
    {


        private IHttpActionResult Get([FromUri] string mediaTypeExtension,[FromUri] int id,[FromUri] string exclude = null)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetAvailableByMediaTypeExtension([FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetClosestByMediaTypeExtension([FromUri] string mediaTypeExtension,[FromUri] decimal lat,[FromUri] decimal iplong)
        {
            // put you code here
            return Ok();
        }

    }

    public partial class HelpController : ApiController
    {


        private IHttpActionResult Get([FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetLanguagesByMediaTypeExtension([FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetPrivacyByMediaTypeExtension([FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }


        private IHttpActionResult GetTosByMediaTypeExtension([FromUri] string mediaTypeExtension)
        {
            // put you code here
            return Ok();
        }

    }

    public partial class ApplicationRateLimitStatusMediaTypeExtensionController : ApiController
    {


        private IHttpActionResult Get([FromUri] string mediaTypeExtension,[FromUri] string resources = null)
        {
            // put you code here
            return Ok();
        }

    }

}