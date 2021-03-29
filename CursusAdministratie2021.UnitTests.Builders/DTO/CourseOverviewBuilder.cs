using CursusAdministratie2021.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.UnitTests.Builders.DTO {
    public class CourseOverviewBuilder {
        private string _title;
        private int _duration;
        private DateTime _startDate;
        private int _editionId;
        private int _enrollmentsCount;

        private CourseOverview companyEmployee;

        public static CourseOverviewBuilder Default() {
            return new CourseOverviewBuilder();
        }

        public CourseOverviewBuilder Simple() {
            return Default()
                .WithTitle("Not Interesting Title")
                .WithDuration(5)
                .WithStartDate(new DateTime(2001,01,01));
        }

        public CourseOverviewBuilder Typical() {
            return Default()
                .Simple()
                .WithEditionId(1)
                .WithEnrollmentsCount(1);
        }

        public CourseOverview Build() {
            if (companyEmployee is null) {
                companyEmployee = new CourseOverview() {
                    Title = _title,
                    Duration = _duration,
                    StartDate = _startDate,
                    EditionId = _editionId,
                    EnrollmentsCount = _enrollmentsCount
                };
            }
            return companyEmployee;
        }
        public CourseOverviewBuilder WithTitle(string title) {
            _title = title;
            return this;
        }
        public CourseOverviewBuilder WithDuration(int duration) {
            _duration = duration;
            return this;
        }
        public CourseOverviewBuilder WithStartDate(DateTime startDate) {
            _startDate = startDate;
            return this;
        }
        public CourseOverviewBuilder WithEditionId(int editionId) {
            _editionId = editionId;
            return this;
        }
        public CourseOverviewBuilder WithEnrollmentsCount(int enrollmentsCount) {
            _enrollmentsCount = enrollmentsCount;
            return this;
        }
    }
}
