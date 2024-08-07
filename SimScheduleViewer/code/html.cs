using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimScheduleViewer.code
{
	public class writeHTML01
	{
		public static string makeHTML(List<MyLearningItem> MLIList)
		{
			code.globs.NoHTMLItems = MLIList.Count;
			string css = @"<style>
						body {
							  width: 100vw;
							  height: 100vh;
							  display: flex;
							  align-items: center;
							  justify-content: center;
							  background:url(""background.jpg"");
							  background-size: cover;
							  overflow: hidden;
  
							}
							.scrolling-wrapper {
							 display: flex;
							  flex-wrap: nowrap;
							  overflow-x: auto;

							  .card {
								flex: 0 0 auto;
							  }
							}
							.card {
							  display: grid;
							  grid-template-columns: 300px;
							  grid-template-rows: 210px 250px 20px;
							  grid-template-areas: ""image"" ""text"" ""stats"";
							  border-radius: 18px;
							  background: #1d1d1d;
							  color:white;
							  box-shadow: 5px 5px 15px rgba(0,0,0,0.9);
							  font-family: roboto;
							  text-align: justify;
							  cursor: pointer;
							  margin:30px;
							  transform-style: preserve-3d;
							  transform: perspective(1000px);
							}
							.rgb::after {
							  content:"""";
							  background: linear-gradient(45deg,
							  #ff0000 0%, 
							  #ff9a00 10%,
							  #d0de21 20%,
							  #4fdc4a 30%, 
							  #3fdad8 40%,
							  #2fc9e2 50%, 
							  #1c7fee 60%, 
							  #5f15f2 70%, 
							  #ba0cf8 80%, 
							  #fb07d9 90%, 
							  #ff0000 100%  )
							  repeat 0% 0% / 300% 100%;
							  position: absolute;
							  inset: -3px;
							  border-radius: 16px;
							  filter: blur(8px);
							  transform: translateZ(-1px); /*or z-index */
							  animation: rgb 6s linear infinite;
							}
							@keyframes rgb {
							  0% {background-position: 0% 50%;}
							  50% {background-position: 100% 50%;}
							  100% {background-position: 0% 50%;}
							}
							.js-tilt-glare {
							  border-radius: 18px;
							}
							.card-image {
							  grid-area: image;
							  background: linear-gradient(#fff0 0%, #fff0 70%, #1d1d1d 100%),url(""img1.jpg"");
							  border-top-left-radius: 15px;
							  border-top-right-radius: 15px;
							  background-size: cover;
							}

							.card-text {
							  grid-area: text;
							  margin: 25px;
							  transform: translateZ(30px);
							}
							.card-text .date {
							  color: rgb(255, 7, 110);
							  font-size:13px;
							}
							.card-text p {
							  color: grey;
							  font-size:14px;
							  font-weight: 300;
							}
							.card-text h2 {
							  margin-top:0px;
							  font-size:28px;
							}
							.card-stats {
							  grid-area: stats; 
							  display: grid;
							  grid-template-columns: 1fr 1fr 1fr;
							  grid-template-rows: 1fr;
							  border-bottom-left-radius: 15px;
							  border-bottom-right-radius: 15px;
							  background: rgb(255, 7, 110);
							}
							.card-stats .stat {
							  padding:10px;
							  display: flex;
							  align-items: center;
							  justify-content: center;
							  flex-direction: column;
							  color: white;
							}
							.card-stats .border {
							  border-left: 1px solid rgb(172, 26, 87);
							  border-right: 1px solid rgb(172, 26, 87);
							}
							.card-stats .value{
							  font-size:22px;
							  font-weight: 500;
							}
							.card-stats .value sup{
							  font-size:12px;
							}
							.card-stats .type{
							  font-size:11px;
							  font-weight: 300;
							  text-transform: uppercase;
							}


							/*card2*/
							.card-image.card2 {
							  background: linear-gradient(#fff0 0%, #fff0 70%, #1d1d1d 100%),url(""img2.jpg"");
							  background-size: cover;
							}
							.card-text.card2 .date {
							  color: rgb(255, 77, 7);
							}
							.card-stats.card2 .border {
							  border-left: 1px solid rgb(185, 67, 20);
							  border-right: 1px solid rgb(185, 67, 20);
							}
							.card-stats.card2 {
							  background: rgb(255, 77, 7);
							}
							/*card3*/
							.card-image.card3 {
							  background: linear-gradient(#fff0 0%, #fff0 70%, #1d1d1d 100%),url(""img3.jpg"");
							  background-size: cover;
							}
							.card-text.card3 .date {
							  color: rgb(0, 189, 63);
							}
							.card-stats.card3 .border {
							  border-left: 1px solid rgb(14, 122, 50);
							  border-right: 1px solid rgb(14, 122, 50);
							}
							.card-stats.card3 {
							  background: rgb(0, 189, 63);
							}
							/*card4*/
							.card-image.card4 {
							  background: linear-gradient(#fff0 0%, #fff0 70%, #1d1d1d 100%),url(""img4.jpg"");
							  background-size: cover;
							}
							.card-text.card4 .date {
							  color: rgb(0, 189, 63);
							}
							.card-stats.card4 .border {
							  border-left: 1px solid rgb(14, 122, 50);
							  border-right: 1px solid rgb(14, 122, 50);
							}
							.card-stats.card4 {
							  background: rgb(0, 189, 63);
							}
							/*card5*/
							.card-image.card5 {
							  background: linear-gradient(#fff0 0%, #fff0 70%, #1d1d1d 100%),url(""img5.jpg"");
							  background-size: cover;
							}
							.card-text.card5 .date {
							  color: rgb(0, 189, 63);
							}
							.card-stats.card5 .border {
							  border-left: 1px solid rgb(14, 122, 50);
							  border-right: 1px solid rgb(14, 122, 50);
							}
							.card-stats.card5 {
							  background: rgb(0, 189, 63);
							}
							/*card6*/
							.card-image.card6 {
							  background: linear-gradient(#fff0 0%, #fff0 70%, #1d1d1d 100%),url(""img6.jpg"");
							  background-size: cover;
							}
							.card-text.card6 .date {
							  color: rgb(0, 189, 63);
							}
							.card-stats.card6 .border {
							  border-left: 1px solid rgb(14, 122, 50);
							  border-right: 1px solid rgb(14, 122, 50);
							}
							.card-stats.card6 {
							  background: rgb(0, 189, 63);
							}
							/*card7*/
							.card-image.card7 {
							  background: linear-gradient(#fff0 0%, #fff0 70%, #1d1d1d 100%),url(""img7.jpg"");
							  background-size: cover;
							}
							.card-text.card7 .date {
							  color: rgb(0, 189, 63);
							}
							.card-stats.card7 .border {
							  border-left: 1px solid rgb(14, 122, 50);
							  border-right: 1px solid rgb(14, 122, 50);
							}
							.card-stats.card7 {
							  background: rgb(0, 189, 63);
							}
							/*card8*/
							.card-image.card8 {
							  background: linear-gradient(#fff0 0%, #fff0 70%, #1d1d1d 100%),url(""img8.jpg"");
							  background-size: cover;
							}
							.card-text.card8 .date {
							  color: rgb(0, 189, 63);
							}
							.card-stats.card8 .border {
							  border-left: 1px solid rgb(14, 122, 50);
							  border-right: 1px solid rgb(14, 122, 50);
							}
							.card-stats.card8 {
							  background: rgb(0, 189, 63);
							}
							/*card9*/
							.card-image.card9 {
							  background: linear-gradient(#fff0 0%, #fff0 70%, #1d1d1d 100%),url(""img9.jpg"");
							  background-size: cover;
							}
							.card-text.card9 .date {
							  color: rgb(0, 189, 63);
							}
							.card-stats.card9 .border {
							  border-left: 1px solid rgb(14, 122, 50);
							  border-right: 1px solid rgb(14, 122, 50);
							}
							.card-stats.card9 {
							  background: rgb(0, 189, 63);
							}
							</style>";
			string beginning = @"<!DOCTYPE html>
				<html>
				  <head>
					<meta charset=""UTF-8"" />" + css + @"
					<script>
					function pageScroll() {
						window.scrollBy(0,1);
						scrolldelay = setTimeout(pageScroll,10);
					}
					</script>
				  </head>
				  <body>
					<div class=""scrolling-wrapper"">
					 ";
			string middle = "";
			Random rnd = new Random();

			foreach (var x in MLIList)
			{
				int rndint = rnd.Next(1, 9);
				if (DateTime.Now > x.StartTime && DateTime.Now < x.EndTime)
				{
					middle = middle + @"<div class=""card rgb"">
						  <div class=""card-image card2"" ></div> <!--" + rndint + @"-->
						  <div class=""card-text card2"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>In Progress Now:</br>" + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
				}
				else if (DateTime.Now > x.StartTime)
				{
					switch (rndint)
					{
						case 1:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card2"" ></div> <!--" + rndint + @"-->
						  <div class=""card-text card2"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>Just Finished: " + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}
						case 2:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card2"" ></div> <!--" + rndint + @"-->
						  <div class=""card-text card2"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>Just Finished: " + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}
						case 3:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card3"" ></div> <!--" + rndint + @"-->
						  <div class=""card-text card3"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>Just Finished: " + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}
						case 4:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card4"" ></div> <!--" + rndint + @"-->
						  <div class=""card-text card4"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>Just Finished: " + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}
						case 5:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card5"" ></div> <!--" + rndint + @"-->
						  <div class=""card-text card5"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>Just Finished: " + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}
						case 6:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card6"" ></div> <!--" + rndint + @"-->
						  <div class=""card-text card6"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>Just Finished: " + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}
						case 7:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card7"" ></div> <!--" + rndint + @"-->
						  <div class=""card-text card7"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>Just Finished: " + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}
						case 8:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card8"" ></div> <!--" + rndint + @"-->
						  <div class=""card-text card8"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>Just Finished: " + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}
						case 9:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card9"" ></div> <!--" + rndint + @"-->
						  <div class=""card-text card9"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>Just Finished: " + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}
						default:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card2"" ></div> <!--" + rndint + @"-->
						  <div class=""card-text card2"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>Just Finished: " + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}

					}

				}
				else if(DateTime.Now < x.StartTime)
				{
					switch (rndint)
					{
						case 1:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card2"" ></div> <!--" + rndint + @"-->
						  <div class=""card-text card2"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>Coming up: " + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}
						case 2:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card2"" ></div> <!--" + rndint + @"-->
						  <div class=""card-text card2"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>Coming up: " + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}
						case 3:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card3"" ></div> <!--" + rndint + @"-->
						  <div class=""card-text card3"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>Coming up: " + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}
						case 4:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card4"" ></div> <!--" + rndint + @"-->
						  <div class=""card-text card4"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>Coming up: " + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}
						default:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card3"" ></div> <!--" + rndint + @"-->
						  <div class=""card-text card3"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>Coming up: " + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}

					}

				}
			}
			string endd = @" </div>
						<script src=""vanilla-tilt.min.js""></script>
						<script>
						  VanillaTilt.init(document.querySelectorAll("".card""),{
							glare: true,
							reverse: true,
							""max-glare"": 0.15
						  });
						</script>
					</div>
				  </body>
				</html><!-- Generated at: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + @"-->";
			string ret = beginning + middle + endd;
			return ret;
		}
	}
	public class writeHTML02
	{
		public static string makeHTML(List<MyLearningItem> MLIList)
		{
			int i = 0;
			string css = @"<style>
@charset ""UTF-8"";
/*PEN STYLES*/
* {
  box-sizing: border-box;
}

body {
  background: #f1f1f1;
  margin: 2rem;
}

.blog-card {
  display: flex;
  flex-direction: column;
  margin: 1rem auto;
  box-shadow: 0 3px 7px -1px rgba(0, 0, 0, 0.1);
  margin-bottom: 1.6%;
  background: #fff;
  line-height: 1.4;
  font-family: sans-serif;
  border-radius: 5px;
  overflow: hidden;
  z-index: 0;
}
.blog-card a {
  color: inherit;
}
.blog-card a:hover {
  color: #5ad67d;
}
.blog-card:hover .photo {
  transform: scale(1.3) rotate(3deg);
}
.blog-card .meta {
  position: relative;
  z-index: 0;
  height: 200px;
}
.blog-card .photo {
  position: absolute;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  background-size: cover;
  background-position: center;
  transition: transform 0.2s;
}
.blog-card .details,
.blog-card .details ul {
  margin: auto;
  padding: 0;
  list-style: none;
}
.blog-card .details {
  position: absolute;
  top: 0;
  bottom: 0;
  left: -100%;
  margin: auto;
  transition: left 0.2s;
  background: rgba(0, 0, 0, 0.6);
  color: #fff;
  padding: 10px;
  width: 100%;
  font-size: 0.9rem;
}
.blog-card .details a {
  -webkit-text-decoration: dotted underline;
          text-decoration: dotted underline;
}
.blog-card .details ul li {
  display: inline-block;
}
.blog-card .details .author:before {
  font-family: FontAwesome;
  margin-right: 10px;
  content: """";
}
.blog-card .details .date:before {
  font-family: FontAwesome;
  margin-right: 10px;
  content: """";
}
.blog-card .details .tags ul:before {
  font-family: FontAwesome;
  content: """";
  margin-right: 10px;
}
.blog-card .details .tags li {
  margin-right: 2px;
}
.blog-card .details .tags li:first-child {
  margin-left: -4px;
}
.blog-card .description {
  padding: 1rem;
  background: #fff;
  position: relative;
  z-index: 1;
}
.blog-card .description h1,
.blog-card .description h2 {
  font-family: Poppins, sans-serif;
}
.blog-card .description h1 {
  line-height: 1;
  margin: 0;
  font-size: 1.7rem;
}
.blog-card .description h2 {
  font-size: 1rem;
  font-weight: 300;
  text-transform: uppercase;
  color: #a2a2a2;
  margin-top: 5px;
}
.blog-card .description .read-more {
  text-align: right;
}
.blog-card .description .read-more a {
  color: #5ad67d;
  display: inline-block;
  position: relative;
}
.blog-card .description .read-more a:after {
  content: """";
  font-family: FontAwesome;
  margin-left: -10px;
  opacity: 0;
  vertical-align: middle;
  transition: margin 0.3s, opacity 0.3s;
}
.blog-card .description .read-more a:hover:after {
  margin-left: 5px;
  opacity: 1;
}
.blog-card p {
  position: relative;
  margin: 1rem 0 0;
}
.blog-card p:first-of-type {
  margin-top: 1.25rem;
}
.blog-card p:first-of-type:before {
  content: """";
  position: absolute;
  height: 5px;
  background: #5ad67d;
  width: 35px;
  top: -0.75rem;
  border-radius: 3px;
}
.blog-card:hover .details {
  left: 0%;
}
@media (min-width: 640px) {
  .blog-card {
    flex-direction: row;
    max-width: 700px;
  }
  .blog-card .meta {
    flex-basis: 40%;
    height: auto;
  }
  .blog-card .description {
    flex-basis: 60%;
  }
  .blog-card .description:before {
    transform: skewX(-3deg);
    content: """";
    background: #fff;
    width: 30px;
    position: absolute;
    left: -10px;
    top: 0;
    bottom: 0;
    z-index: -1;
  }
  .blog-card.alt {
    flex-direction: row-reverse;
  }
  .blog-card.alt .description:before {
    left: inherit;
    right: -10px;
    transform: skew(3deg);
  }
  .blog-card.alt .details {
    padding-left: 25px;
  }
}</style>";
			string beginning = @"<!DOCTYPE html>
				<html lang=""en"" dir=""ltr"">
				  <head>
					<meta charset=""UTF-8"" />" + css + @"
					<script>
					function pageScroll() {
						window.scrollBy(0,1);
						scrolldelay = setTimeout(pageScroll,10);
					}
					</script>
				  </head>
				  <body>
<div id=""top""></div>
					<div class=""container"">
					 ";
			string middle = "";
			Random rnd = new Random();
			//int rndint = rnd.Next(1, 5);
			foreach (var x in MLIList)
			{
				bool isEven = i % 2 == 0;
				int rndint = rnd.Next(1, 5);
				if (isEven) { middle = middle + @" <div class=""blog-card"">"; } else { middle = middle + @" <div class=""blog-card alt"">"; }
				i = i + 1;
				if (DateTime.Now > x.StartTime && DateTime.Now < x.EndTime)
				{
					middle = middle + @"
    <div class=""meta"">
      <div class=""photo"" style=""background-image: url(img1.jpg)""></div>
      <ul class=""details"">
        <li class=""author""><a href=""#"">" + x.InstructorFirstName + " " + x.InstructorLastName + @"</a></li>
        <li class=""date"">" + x.StartTime.ToString("F") + @"</li>
        <li class=""tags"">
          <ul>
            <li><a href=""#"">" + x.ItemType + @"</a></li>
          </ul>
        </li>
      </ul>
    </div>
    <div class=""description"">
      <h1><u>In Progress:</u> " + x.Description + @"</h1>
      <h2>Item ID: " + x.ItemID + "  |  Class ID: " + x.ClassID + @"</h2>
      <p>" + "Class begins at: " + x.StartTime.ToString("F") + "</br>Class ends at: " + x.EndTime.ToString("F") + "</br>Duration: " + x.TotalHours.ToString() + " hours</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + "</br>Location: " + x.PrimaryLocation + @"</p>
      <p class=""read-more"">
        <a href=""https://entergy.sharepoint.com/sites/LearningManagementSystem/Pages/MyLearning-Training-Documents.aspx"">My Learning</a>
      </p>
    </div>
  </div>
</div>;";
				}
				else
				{

					switch (rndint)
					{
						case 1:
							{
								middle = middle + @"
    <div class=""meta"">
      <div class=""photo"" style=""background-image: url(img1.jpg)""></div>
      <ul class=""details"">
        <li class=""author""><a href=""#"">" + x.InstructorFirstName + " " + x.InstructorLastName + @"</a></li>
        <li class=""date"">" + x.StartTime.ToString("F") + @"</li>
        <li class=""tags"">
          <ul>
            <li><a href=""#"">" + x.ItemType + @"</a></li>
          </ul>
        </li>
      </ul>
    </div>
    <div class=""description"">
      <h1>" + x.Description + @"</h1>
      <h2>Item ID: " + x.ItemID + "  |  Class ID: " + x.ClassID + @"</h2>
      <p>" + "Class begins at: " + x.StartTime.ToString("F") + "</br>Class ends at: " + x.EndTime.ToString("F") + "</br>Duration: " + x.TotalHours.ToString() + " hours</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + "</br>Location: " + x.PrimaryLocation + @"</p>
      <p class=""read-more"">
        <a href=""https://entergy.sharepoint.com/sites/LearningManagementSystem/Pages/MyLearning-Training-Documents.aspx"">My Learning</a>
      </p>
    </div>
  </div>";
								break;
							}
						case 2:
							{
								middle = middle + @"
    <div class=""meta"">
      <div class=""photo"" style=""background-image: url(img2.jpg)""></div>
      <ul class=""details"">
        <li class=""author""><a href=""#"">" + x.InstructorFirstName + " " + x.InstructorLastName + @"</a></li>
        <li class=""date"">" + x.StartTime.ToString("F") + @"</li>
        <li class=""tags"">
          <ul>
            <li><a href=""#"">" + x.ItemType + @"</a></li>
          </ul>
        </li>
      </ul>
    </div>
    <div class=""description"">
      <h1>" + x.Description + @"</h1>
      <h2>Item ID: " + x.ItemID + "  |  Class ID: " + x.ClassID + @"</h2>
      <p>" + "Class begins at: " + x.StartTime.ToString("F") + "</br>Class ends at: " + x.EndTime.ToString("F") + "</br>Duration: " + x.TotalHours.ToString() + " hours</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + "</br>Location: " + x.PrimaryLocation + @"</p>
      <p class=""read-more"">
        <a href=""https://entergy.sharepoint.com/sites/LearningManagementSystem/Pages/MyLearning-Training-Documents.aspx"">My Learning</a>
      </p>
    </div>
  </div>";
								break;
							}
						case 3:
							{
								middle = middle + @"
    <div class=""meta"">
      <div class=""photo"" style=""background-image: url(img3.jpg)""></div>
      <ul class=""details"">
        <li class=""author""><a href=""#"">" + x.InstructorFirstName + " " + x.InstructorLastName + @"</a></li>
        <li class=""date"">" + x.StartTime.ToString("F") + @"</li>
        <li class=""tags"">
          <ul>
            <li><a href=""#"">" + x.ItemType + @"</a></li>
          </ul>
        </li>
      </ul>
    </div>
    <div class=""description"">
      <h1>" + x.Description + @"</h1>
      <h2>Item ID: " + x.ItemID + "  |  Class ID: " + x.ClassID + @"</h2>
      <p>" + "Class begins at: " + x.StartTime.ToString("F") + "</br>Class ends at: " + x.EndTime.ToString("F") + "</br>Duration: " + x.TotalHours.ToString() + " hours</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + "</br>Location: " + x.PrimaryLocation + @"</p>
      <p class=""read-more"">
        <a href=""https://entergy.sharepoint.com/sites/LearningManagementSystem/Pages/MyLearning-Training-Documents.aspx"">My Learning</a>
      </p>
    </div>
  </div>";
								break;
							}
						case 4:
							{
								middle = middle + @"
    <div class=""meta"">
      <div class=""photo"" style=""background-image: url(img4.jpg)""></div>
      <ul class=""details"">
        <li class=""author""><a href=""#"">" + x.InstructorFirstName + " " + x.InstructorLastName + @"</a></li>
        <li class=""date"">" + x.StartTime.ToString("F") + @"</li>
        <li class=""tags"">
          <ul>
            <li><a href=""#"">" + x.ItemType + @"</a></li>
          </ul>
        </li>
      </ul>
    </div>
    <div class=""description"">
      <h1>" + x.Description + @"</h1>
      <h2>Item ID: " + x.ItemID + "  |  Class ID: " + x.ClassID + @"</h2>
      <p>" + "Class begins at: " + x.StartTime.ToString("F") + "</br>Class ends at: " + x.EndTime.ToString("F") + "</br>Duration: " + x.TotalHours.ToString() + " hours</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + "</br>Location: " + x.PrimaryLocation + @"</p>
      <p class=""read-more"">
        <a href=""https://entergy.sharepoint.com/sites/LearningManagementSystem/Pages/MyLearning-Training-Documents.aspx"">My Learning</a>
      </p>
    </div>
  </div>";
								break;
							}
						case 5:
							{
								middle = middle + @"
    <div class=""meta"">
      <div class=""photo"" style=""background-image: url(img5.jpg)""></div>
      <ul class=""details"">
        <li class=""author""><a href=""#"">" + x.InstructorFirstName + " " + x.InstructorLastName + @"</a></li>
        <li class=""date"">" + x.StartTime.ToString("F") + @"</li>
        <li class=""tags"">
          <ul>
            <li><a href=""#"">" + x.ItemType + @"</a></li>
          </ul>
        </li>
      </ul>
    </div>
    <div class=""description"">
      <h1>" + x.Description + @"</h1>
      <h2>Item ID: " + x.ItemID + "  |  Class ID: " + x.ClassID + @"</h2>
      <p>" + "Class begins at: " + x.StartTime.ToString("F") + "</br>Class ends at: " + x.EndTime.ToString("F") + "</br>Duration: " + x.TotalHours.ToString() + " hours</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + "</br>Location: " + x.PrimaryLocation + @"</p>
      <p class=""read-more"">
        <a href=""https://entergy.sharepoint.com/sites/LearningManagementSystem/Pages/MyLearning-Training-Documents.aspx"">My Learning</a>
      </p>
    </div>
  </div>";
								break;
							}
						default:
							{
								middle = middle + @"
    <div class=""meta"">
      <div class=""photo"" style=""background-image: url(img1.jpg)""></div>
      <ul class=""details"">
        <li class=""author""><a href=""#"">" + x.InstructorFirstName + " " + x.InstructorLastName + @"</a></li>
        <li class=""date"">" + x.StartTime.ToString() + @"</li>
        <li class=""tags"">
          <ul>
            <li><a href=""#"">" + x.ItemType + @"</a></li>
          </ul>
        </li>
      </ul>
    </div>
    <div class=""description"">
      <h1>" + x.Description + @"</h1>
      <h2>Item ID: " + x.ItemID + "  |  Class ID: " + x.ClassID + @"</h2>
      <p>" + "Class begins at: " + x.StartTime.ToString() + "</br>Class ends at: " + x.EndTime.ToString() + "</br>Duration: " + x.TotalHours.ToString() + " hours</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + "</br>Location: " + x.PrimaryLocation + @"</p>
      <p class=""read-more"">
        <a href=""https://entergy.sharepoint.com/sites/LearningManagementSystem/Pages/MyLearning-Training-Documents.aspx"">My Learning</a>
      </p>
    </div>
  </div>";
								break;
							}

					}

				}
			}
			string endd = @" </div>
						<script src=""vanilla-tilt.min.js""></script>
						<script>
						  VanillaTilt.init(document.querySelectorAll("".card""),{
							glare: true,
							reverse: true,
							""max-glare"": 0.15
						  });
						</script>
<div id=""bottom""></div>
				  </body>
				</html>";
			string ret = beginning + middle + endd;
			return ret;
		}
	}

	public class writeHTML03
	{
		public static string makeHTML(List<MyLearningItem> MLIList)
		{
			string css = @"<style>
body {
							  width: 100vw;
							  
							  display: flex;
							  align-items: center;
							  justify-content: center;
							  background:url(""background.jpg"");
							  background-size: cover;
							  overflow: hidden;
  
							}
							.scrolling-wrapper {
							 display: flex;
							  flex-flow: row wrap;
							  overflow-x: auto;
							  overflow-y: scroll;

							  .card {
								flex: 0 0 auto;
							  }
							}
							.card {
							  display: grid;
							  grid-template-columns: 300px;
							  grid-template-rows: 210px 250px 20px;
							  grid-template-areas: ""image"" ""text"" ""stats"";
							  border-radius: 18px;
							  background: #1d1d1d;
							  color:white;
							  box-shadow: 5px 5px 15px rgba(0,0,0,0.9);
							  font-family: roboto;
							  text-align: justify;
							  cursor: pointer;
							  margin:30px;
							  transform-style: preserve-3d;
							  transform: perspective(1000px);
							}
							.rgb::after {
							  content:"""";
							  background: linear-gradient(45deg,
							  #ff0000 0%, 
							  #ff9a00 10%,
							  #d0de21 20%,
							  #4fdc4a 30%, 
							  #3fdad8 40%,
							  #2fc9e2 50%, 
							  #1c7fee 60%, 
							  #5f15f2 70%, 
							  #ba0cf8 80%, 
							  #fb07d9 90%, 
							  #ff0000 100%  )
							  repeat 0% 0% / 300% 100%;
							  position: absolute;
							  inset: -3px;
							  border-radius: 16px;
							  filter: blur(8px);
							  transform: translateZ(-1px); /*or z-index */
							  animation: rgb 6s linear infinite;
							}
							@keyframes rgb {
							  0% {background-position: 0% 50%;}
							  50% {background-position: 100% 50%;}
							  100% {background-position: 0% 50%;}
							}
							.js-tilt-glare {
							  border-radius: 18px;
							}
							.card-image {
							  grid-area: image;
							  background: linear-gradient(#fff0 0%, #fff0 70%, #1d1d1d 100%),url(""img1.jpg"");
							  border-top-left-radius: 15px;
							  border-top-right-radius: 15px;
							  background-size: cover;
							}

							.card-text {
							  grid-area: text;
							  margin: 25px;
							  transform: translateZ(30px);
							}
							.card-text .date {
							  color: rgb(255, 7, 110);
							  font-size:13px;
							}
							.card-text p {
							  color: grey;
							  font-size:14px;
							  font-weight: 300;
							}
							.card-text h2 {
							  margin-top:0px;
							  font-size:28px;
							}
							.card-stats {
							  grid-area: stats; 
							  display: grid;
							  grid-template-columns: 1fr 1fr 1fr;
							  grid-template-rows: 1fr;
							  border-bottom-left-radius: 15px;
							  border-bottom-right-radius: 15px;
							  background: rgb(255, 7, 110);
							}
							.card-stats .stat {
							  padding:10px;
							  display: flex;
							  align-items: center;
							  justify-content: center;
							  flex-direction: column;
							  color: white;
							}
							.card-stats .border {
							  border-left: 1px solid rgb(172, 26, 87);
							  border-right: 1px solid rgb(172, 26, 87);
							}
							.card-stats .value{
							  font-size:22px;
							  font-weight: 500;
							}
							.card-stats .value sup{
							  font-size:12px;
							}
							.card-stats .type{
							  font-size:11px;
							  font-weight: 300;
							  text-transform: uppercase;
							}


							/*card2*/
							.card-image.card2 {
							  background: linear-gradient(#fff0 0%, #fff0 70%, #1d1d1d 100%),url(""img2.jpg"");
							  background-size: cover;
							}
							.card-text.card2 .date {
							  color: rgb(255, 77, 7);
							}
							.card-stats.card2 .border {
							  border-left: 1px solid rgb(185, 67, 20);
							  border-right: 1px solid rgb(185, 67, 20);
							}
							.card-stats.card2 {
							  background: rgb(255, 77, 7);
							}
							/*card3*/
							.card-image.card3 {
							  background: linear-gradient(#fff0 0%, #fff0 70%, #1d1d1d 100%),url(""img3.jpg"");
							  background-size: cover;
							}
							.card-text.card3 .date {
							  color: rgb(0, 189, 63);
							}
							.card-stats.card3 .border {
							  border-left: 1px solid rgb(14, 122, 50);
							  border-right: 1px solid rgb(14, 122, 50);
							}
							.card-stats.card3 {
							  background: rgb(0, 189, 63);
							}
							/*card4*/
							.card-image.card4 {
							  background: linear-gradient(#fff0 0%, #fff0 70%, #1d1d1d 100%),url(""img4.jpg"");
							  background-size: cover;
							}
							.card-text.card4 .date {
							  color: rgb(0, 189, 63);
							}
							.card-stats.card4 .border {
							  border-left: 1px solid rgb(14, 122, 50);
							  border-right: 1px solid rgb(14, 122, 50);
							}
							.card-stats.card4 {
							  background: rgb(0, 189, 63);
							}
							</style>";
			string beginning = @"<!DOCTYPE html>
				<html>
				  <head>
					<meta charset=""UTF-8"" />" + css + @"
					<script>
					function pageScroll() {
						window.scrollBy(0,1);
						scrolldelay = setTimeout(pageScroll,10);
					}
					</script>
				  </head>
				  <body>
					<div class=""scrolling-wrapper"">
					 ";
			string middle = "";
			Random rnd = new Random();

			foreach (var x in MLIList)
			{
				int rndint = rnd.Next(1, 5);
				if (DateTime.Now > x.StartTime && DateTime.Now < x.EndTime)
				{
					middle = middle + @"<div class=""card rgb"">
						  <div class=""card-image card2"" ></div>
						  <div class=""card-text card2"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>In Progress Now:</br>" + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
				}
				else
				{
					switch (rndint)
					{
						case 1:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image"" ></div><!--" + rndint + @"-->
						  <div class=""card-text"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>" + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}
						case 2:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card2"" ></div><!--" + rndint + @"-->
						  <div class=""card-text card2"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>" + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}
						case 3:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card3"" ></div><!--" + rndint + @"-->
						  <div class=""card-text card3"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>" + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}
						case 4:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card4"" ></div><!--" + rndint + @"-->
						  <div class=""card-text card4"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>" + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}
						default:
							{
								middle = middle + @"<div class=""card"">
						  <div class=""card-image card2"" ></div><!--" + rndint + @"-->
						  <div class=""card-text card2"">
							<span class=""date"">" + x.StartTime.ToString() + @"</span>
							<h2>" + x.ItemID + @"</h2>
							<p>" + x.Description + "</br>Starts: " + x.StartTime + "</br>Ends: " + x.EndTime + "</br>Hours: " + x.TotalHours + "</br>Instructor: " + x.InstructorFirstName + " " + x.InstructorLastName + @"</p>
						  </div>
						</div>";
								break;
							}

					}

				}
			}
			string endd = @" </div>
						<script src=""vanilla-tilt.min.js""></script>
						<script>
						  VanillaTilt.init(document.querySelectorAll("".card""),{
							glare: true,
							reverse: true,
							""max-glare"": 0.15
						  });
						</script>
					</div>
				  </body>
				</html>";
			string ret = beginning + middle + endd;
			return ret;
		}
	}

	public class writeHTML04
	{
		public static string makeHTML(List<MyLearningItem> MLIList)
		{
			string css = @"<style>
body, div, span, applet, object, iframe,
h1, h2, h3, h4, h5, h6, p, blockquote, pre,
a, abbr, acronym, address, big, cite, code,
del, dfn, em, img, ins, kbd, q, s, samp,
small, strike, strong, sub, sup, tt, var,
b, u, i,dl, dt, dd, ol, ul, li,
fieldset, form, label, legend,
table, caption, tbody, tfoot, thead, tr, th, td,
article, aside, canvas, details, embed, 
figure, figcaption, footer, header, hgroup, 
menu, nav, output, section, summary,
time, audio {
  margin: 0;
  padding: 0;
  border: 0;
  font-size: 100%;
  font: inherit;
  vertical-align: baseline;
  font-family: 'Montserrat', sans-serif;
  color: #D1D1D1;
}

.container {
  height:720px;
  width: 900px;
  display: inline-flex;
}

.timings {
  text-align: right;
  padding-right: 10px;
  width: 100px;
  height: 720px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  background-color: white;
  font-size: 0.5em;
  box-sizing: border-box
}

.timings span {
  font-size: 200%;
  color: #696969;
}

.days {
  height: 720px;
  width: 620px;
  padding: 0px 10px;
  background-color: #F0F0F0;
  border-color: #D1D1D1;
  border-style: solid;
  border-width: 1px;
  box-sizing: border-box;
}

.event {
  background-color: white;
  border-style: solid;
  border-width: 1px;
  border-left-width: 5px;
  border-color: #D1D1D1;
  border-left-color: #57b986;
  padding-left: 10px;
  padding-top: 5px;
  position: absolute;
  font-size: 0.5em;
  box-sizing: border-box;
}

.event .title {
  color: #57b986;
  font-size: 200%;
}
							</style>";
			string html = @"<!DOCTYPE html>
<html>
<head>
  <title>Cool Calendar</title>
  <link href=""./style.css"" rel=""stylesheet"">
  <link href=""https://fonts.googleapis.com/css?family=Montserrat"" rel=""stylesheet"">
</head>

<body>
<div class =""container"">
  <div class=""timings"">
    <div> <span> 9:00 </span> AM </div>
    <div> 9:30 </div>
    <div> <span> 10:00 </span>AM </div>
    <div> 10:30 </div>
    <div> <span> 11:00 </span>AM </div>
    <div> 11:30 </div>
    <div> <span> 12:00 </span>PM </div>
    <div> 12:30 </div>
    <div> <span> 1:00 </span>PM </div>
    <div> 1:30 </div>
    <div> <span> 2:00 </span>PM </div>
    <div> 2:30 </div>
    <div> <span> 3:00 </span>PM </div>
    <div> 3:30 </div>
    <div> <span> 4:00 </span>PM </div>
    <div> 4:30 </div>
    <div> <span> 5:00 </span>PM </div>
    <div> 5:30 </div>
    <div> <span> 6:00 </span>PM </div>
    <div> 6:30 </div>
    <div> <span> 7:00 </span>PM </div>
    <div> 7:30 </div>
    <div> <span> 8:00 </span>PM </div>
    <div> 8:30 </div>
    <div> <span> 9:00 </span>PM </div>
  </div>

  <div class=""days"" id=""events"">
  </div>

</div>

<script src=""./script.js""></script>
<script src=""./layOutDay.js""></script>
</body>
</html>";
			string ret = "";
			return ret;
		}
	}
}
