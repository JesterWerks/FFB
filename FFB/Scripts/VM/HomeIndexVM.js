//@@ sourceURL=VM/HomeIndexVM.js

jQuery(document).ready(function () {
    BindViewModel();

});

var Player = function () {
    var self = this;

    self.AvgPointsPerGame = ko.observable();
    self.GameInfo = ko.observable();
    self.Id = ko.observable();
    self.Name = ko.observable();
    self.Position = ko.observable();
    self.Salary = ko.observable();
    self.teamAbbrev = ko.observable();
    self.SelectPlayer = ko.observable(false);
};

var LineUp = function () {
    var self = this;

    self.Id = ko.observable();
    self.QB = ko.observable();
    self.RB1 = ko.observable();
    self.RB2 = ko.observable();
    self.WR1 = ko.observable();
    self.WR2 = ko.observable();
    self.WR3 = ko.observable();
    self.TE = ko.observable();
    self.FLEX = ko.observable();
    self.DST = ko.observable();
    self.TotalSalary = ko.observable();
    self.ProjectedFFP = ko.observable();
};

var Schedule = function () {
    var self = this;

    Id = ko.observable();
    ScheduleTypeId = ko.observable();
    ScheduleTypeName = ko.observable();
    Year = ko.observable();
    Week = ko.observable();
    Note = ko.observable();
};

var Home = function () {
    var self = this;

    self.ShowQB = ko.observable(true);
    self.ShowRB = ko.observable(true);
    self.ShowWR = ko.observable(true);
    self.ShowTE = ko.observable(true);
    self.ShowDST = ko.observable(true);
    self.ShowFLEX = ko.observable(true);

    self.ScheduleList = ko.observableArray([]);
    self.GetScheduleList = function () {
        jQuery.getJSON("../api/HomeAPI/GetSchedules", {}, function (data) {
            var allSchedules = [];
            console.log(data);
        });
    };

    self.Players = ko.observableArray([]);
    self.GetAllPlayersList = function () {
        jQuery.getJSON("../api/HomeAPI/GetAllPlayers", {}, function (data) {
            var allPlayers = [];
            jQuery.each(data, function () {
                var player = new Player();
                player.AvgPointsPerGame = this.AvgPointsPerGame;
                player.GameInfo = this.GameInfo;
                player.Id = this.Id;
                player.Name = this.Name;
                player.Position = this.Position;
                player.Salary = this.Salary;
                player.teamAbbrev = this.teamAbbrev;
                allPlayers.push(player);
               
            });
            self.Players(allPlayers);
        });
    };
    self.GetAllPlayersList();

    self.SelectedPlayersList = ko.observableArray([]);
    
    self.QBSelected = ko.computed(function () { var count = 0; jQuery.each(self.SelectedPlayersList(), function () { if (this.Position == 'QB') { count += 1; } }); return count; });
    self.RBSelected = ko.computed(function () { var count = 0; jQuery.each(self.SelectedPlayersList(), function () { if (this.Position == 'RB') { count += 1; } }); return count; });
    self.WRSelected = ko.computed(function () { var count = 0; jQuery.each(self.SelectedPlayersList(), function () { if (this.Position == 'WR') { count += 1; } }); return count; });
    self.TESelected = ko.computed(function () { var count = 0; jQuery.each(self.SelectedPlayersList(), function () { if (this.Position == 'TE') { count += 1; } }); return count; });
    self.DSTSelected = ko.computed(function () { var count = 0; jQuery.each(self.SelectedPlayersList(), function () { if (this.Position == 'DST') { count += 1; } }); return count; });
    self.FLEXAvailable = ko.computed(function () { return self.RBSelected() + self.WRSelected() + self.TESelected();  });

    self.AllPositionsFilled = ko.pureComputed(function () {
        return (self.QBSelected() >= 1 && self.RBSelected() >= 2 && self.WRSelected() >= 3 && self.TESelected() >= 1 && self.DSTSelected() >= 1 && self.FLEXAvailable() >= 7);
    });
    self.GeneratedLineUps = ko.observableArray([]);
    self.PlayerSelectionComplete = ko.observable(false);
    self.SubmitPlayerList = function () {
        if (self.SelectedPlayersList()) {
            jQuery.ajax("../api/HomeAPI/GenerateLineUps", {
                data: ko.mapping.toJSON(self.SelectedPlayersList()),
                type: "post",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var lineUps = [];
                    jQuery.each(data, function () {
                        console.log(this);
                        var lineup = new LineUp();
                        
                        lineup.Id(this.Id);
                        lineup.QB(this.QB);
                        lineup.RB1(this.RB1);
                        lineup.RB2(this.RB2);
                        lineup.WR1(this.WR1);
                        lineup.WR2(this.WR2);
                        lineup.WR3(this.WR3);
                        lineup.TE(this.TE);
                        lineup.FLEX(this.FLEX);
                        lineup.DST(this.DST);
                        lineup.TotalSalary(this.TotalSalary);
                        lineup.ProjectedFFP(this.ProjectedFFP);
                        lineUps.push(lineup);
                    });
                    self.GeneratedLineUps(lineUps);
                    self.PlayerSelectionComplete(true);
                }
            }).fail(function () { });
        } else {
            console.log("list undefined");
        }
    };
};

function BindViewModel() {

    // create instance of model
    var vm = new Home();

    // setup validation for instance
    //vm.errors = ko.validation.group(vm, { deep: true });

    // apply bindings
    ko.applyBindings(vm);

};