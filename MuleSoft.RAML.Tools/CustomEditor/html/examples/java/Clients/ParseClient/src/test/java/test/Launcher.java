
package test;

import com.onpositive.parse.client.Api;
import com.onpositive.parse.client.security.ApiSecurity;
import com.onpositive.parse.model.ClassGet;
import com.onpositive.parse.model.ClassesQuery;
import com.onpositive.parse.model.GetUsersFormBody;
import com.onpositive.parse.model.UserPost;
import com.onpositive.parse.model.UsersQuery;
import com.onpositive.parse.model.UsersQueryResults;

public class Launcher {

	private static final String APPLICATION_ID = "Your Application ID here";
	private static final String REST_API_KEY = "Your REST API Key here";
	private static final String MASTER_KEY = "Your Master Key here";

	public static void main(String[] args) {
		
		Api parse = new Api();
		
		ApiSecurity sec = parse.security();
		
		//set global security values
		sec.setParamValue("X-Parse-Application-Id", APPLICATION_ID);
		sec.setParamValue("X-Parse-REST-API-Key", REST_API_KEY);
		sec.setParamValue("X-Parse-Master-Key", MASTER_KEY);
		
		//set security values to Common schema
		sec.getCommonSchema().setX_Parse_Application_Id(APPLICATION_ID);
		sec.getCommonSchema().setX_Parse_REST_API_Key(REST_API_KEY);
		
		//set security values to Master schema
		sec.getMasterSchema().setX_Parse_Application_Id(APPLICATION_ID);
		sec.getMasterSchema().setX_Parse_Master_Key(MASTER_KEY);
		
		UserPost user = new UserPost();
		user.username = "UserCreatedByJavaClient_2";
		user.password = "ldsuvlhlhyltrk56nfgbdl";
		//UsersPostResponse createdUser = parse.users.post(user);
		
		ClassesQuery rectangles = parse.classes.className("rectangle").get(null);
		
		String id = rectangles.results.get(0).objectId;
		
		System.out.println("id == " + id);
		
		
		ClassGet rectangle = parse.classes.className("rectangle").objectId(id).get();
		
		String createdAt = rectangle.createdAt;
		
		System.out.println("created at " + createdAt);
		
		UsersQuery users = parse.users.get(null);
		
		System.out.println("Users:");		
		for( UsersQueryResults uqr : users.results ){
			System.out.println(uqr.username);						
		}
		GetUsersFormBody opts = new GetUsersFormBody();
		opts.skip = 3.;
		opts.limit = 2.;
		UsersQuery users2 = parse.users.get(opts);		
		
		System.out.println("Users:");		
		for( UsersQueryResults uqr : users2.results ){
			System.out.println(uqr.username);						
		}
		
	}

}
