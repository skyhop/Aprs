using System.Collections.Generic;

namespace Skyhop.AprsClient
{
    public class AprsFilterBuilder
    {
        public AprsFilterBuilder() {
            _aprsFilters = new List<AprsFilter>();
        }

        public AprsFilterBuilder(AprsFilter[] filters) {
            _aprsFilters = new List<AprsFilter>(filters);
        }

        public AprsFilterBuilder(IEnumerable<AprsFilter> filters) {
            _aprsFilters = new List<AprsFilter>(filters);
        }

        private List<AprsFilter> _aprsFilters { get; set; }

        public AprsFilterBuilder AddFilter(AprsFilter filter) {
            _aprsFilters.Add(filter);
            return this;
        }

        public string GetFilter() {
            string result = "";

            _aprsFilters.ForEach(q => result += $"{q.Result} ");

            result = result.Trim();

            return result;
        }
    }
}
