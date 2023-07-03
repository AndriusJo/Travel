using Travel;
using Travel.Classes.InputModels;
using Travel.Functions;

namespace TravelTest
{
    [TestClass]
    public class LocationCheckTests
    {
        [TestMethod]
        public void Location_IsInside_Polygon()
        {
            var location = new Location("location1", new Coordinates(25.21051562929364, 54.64057937965808));

            var polygon = new Polygon(new List<Coordinates>() {
                 new Coordinates(23.13573603154873, 54.67922829209249),
                 new Coordinates(23.156131289233258, 54.58478594629585),
                 new Coordinates(25.286660938416787,54.5942400514071),
                 new Coordinates(25.429427742209867,54.64619841630662),
                 new Coordinates(25.36416291761924, 54.77109854334182),
                 new Coordinates(25.13573603154873, 55.77109854334182),
                 new Coordinates(23.13573603154873, 54.67922829209249)});

            var region = new Region("region1", new List<Polygon> { polygon });

            var expected = true;
            var actual = LocationCheck.isInRegion(region, location);
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void Location_IsOutside_Polygon()
        {
            var location = new Location("location1", new Coordinates(23.03573603154873, 54.67922829209249));

            var polygon = new Polygon(new List<Coordinates>() {
                 new Coordinates(23.13573603154873, 54.67922829209249),
                 new Coordinates(23.156131289233258, 54.58478594629585),
                 new Coordinates(25.286660938416787,54.5942400514071),
                 new Coordinates(25.429427742209867,54.64619841630662),
                 new Coordinates(25.36416291761924, 54.77109854334182),
                 new Coordinates(25.13573603154873, 55.77109854334182),
                 new Coordinates(23.13573603154873, 54.67922829209249)});

            var region = new Region("region1", new List<Polygon> { polygon });

            var expected = false;
            var actual = LocationCheck.isInRegion(region, location);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Location_IsOnEdgeOf_Polygon()
        {
            var location = new Location("location1", new Coordinates(23.13573603154873, 54.67922829209249));

            var polygon = new Polygon(new List<Coordinates>() {
                 new Coordinates(23.13573603154873, 54.67922829209249),
                 new Coordinates(23.156131289233258, 54.58478594629585),
                 new Coordinates(25.286660938416787,54.5942400514071),
                 new Coordinates(25.429427742209867,54.64619841630662),
                 new Coordinates(25.36416291761924, 54.77109854334182),
                 new Coordinates(25.13573603154873, 55.77109854334182),
                 new Coordinates(23.13573603154873, 54.67922829209249)});

            var region = new Region("region1", new List<Polygon> { polygon });

            var expected = true;
            var actual = LocationCheck.isInRegion(region, location);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Location_HasNullValues()
        {
            var location = new Location("location1", new Coordinates(null, 54.64057937965808));

            var polygon = new Polygon(new List<Coordinates>() {
                 new Coordinates(23.13573603154873, 54.67922829209249),
                 new Coordinates(23.156131289233258, 54.58478594629585),
                 new Coordinates(25.286660938416787,54.5942400514071),
                 new Coordinates(25.429427742209867,54.64619841630662),
                 new Coordinates(25.36416291761924, 54.77109854334182),
                 new Coordinates(25.13573603154873, 55.77109854334182),
                 new Coordinates(23.13573603154873, 54.67922829209249)});

            var region = new Region("region1", new List<Polygon> { polygon });

            Assert.ThrowsException<ArgumentNullException>(() => LocationCheck.isInRegion(region, location));
        }

        [TestMethod]
        public void Region_HasNullValues()
        {
            var location = new Location("location1", new Coordinates(25.21051562929364, 54.64057937965808));

            var polygon = new Polygon(new List<Coordinates>() {
                 new Coordinates(23.13573603154873, 54.67922829209249),
                 new Coordinates(null, 54.58478594629585),
                 new Coordinates(25.286660938416787,54.5942400514071),
                 new Coordinates(25.429427742209867,54.64619841630662),
                 new Coordinates(25.36416291761924, 54.77109854334182),
                 new Coordinates(25.13573603154873, 55.77109854334182),
                 new Coordinates(23.13573603154873, 54.67922829209249)});

            var region = new Region("region1", new List<Polygon> { polygon });

            Assert.ThrowsException<ArgumentNullException>(() => LocationCheck.isInRegion(region, location));
        }
    }
}