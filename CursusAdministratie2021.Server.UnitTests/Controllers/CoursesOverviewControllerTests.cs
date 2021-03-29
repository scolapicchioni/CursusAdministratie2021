using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using CursusAdministratie2021.Shared.Interfaces;
using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Server.Controllers;
using CursusAdministratie2021.UnitTests.Builders.DTO;

namespace CursusAdministratie2021.Server.UnitTests.Controllers {
    public class CoursesOverviewControllerTests {
        Mock<ICoursesOverviewService> coursesOverviewServiceMock;

        public CoursesOverviewControllerTests() {
            coursesOverviewServiceMock = new();
        }
        [Fact]
        public async Task GetCoursesOverview_ShouldReturn_ListOfCourses() {
            List<CourseOverview> expectedCourses;
            expectedCourses = new List<CourseOverview> {
                CourseOverviewBuilder.Default().Typical().Build(),
                CourseOverviewBuilder.Default().Typical().Build(),
                CourseOverviewBuilder.Default().Typical().Build()
            };
            coursesOverviewServiceMock.Setup(cos => cos.GetCoursesOverview()).ReturnsAsync(expectedCourses);

            CoursesOverviewController sut = new CoursesOverviewController(coursesOverviewServiceMock.Object);

            IEnumerable<CourseOverview> actualCourses = await sut.GetCoursesOverview();

            Assert.Equal(expectedCourses, actualCourses);
        }
    }
}
