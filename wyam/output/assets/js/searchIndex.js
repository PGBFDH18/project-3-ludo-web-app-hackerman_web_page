
var camelCaseTokenizer = function (builder) {

  var pipelineFunction = function (token) {
    var previous = '';
    // split camelCaseString to on each word and combined words
    // e.g. camelCaseTokenizer -> ['camel', 'case', 'camelcase', 'tokenizer', 'camelcasetokenizer']
    var tokenStrings = token.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
      var current = cur.toLowerCase();
      if (acc.length === 0) {
        previous = current;
        return acc.concat(current);
      }
      previous = previous.concat(current);
      return acc.concat([current, previous]);
    }, []);

    // return token for each string
    // will copy any metadata on input token
    return tokenStrings.map(function(tokenString) {
      return token.clone(function(str) {
        return tokenString;
      })
    });
  }

  lunr.Pipeline.registerFunction(pipelineFunction, 'camelCaseTokenizer')

  builder.pipeline.before(lunr.stemmer, pipelineFunction)
}
var searchModule = function() {
    var documents = [];
    var idMap = [];
    function a(a,b) { 
        documents.push(a);
        idMap.push(b); 
    }

    a(
        {
            id:0,
            title:"GetMovePiece",
            content:"GetMovePiece",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp/GetMovePiece',
            title:"GetMovePiece",
            description:""
        }
    );
    a(
        {
            id:1,
            title:"Startup",
            content:"Startup",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp/Startup',
            title:"Startup",
            description:""
        }
    );
    a(
        {
            id:2,
            title:"GetPlayerInfo",
            content:"GetPlayerInfo",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp/GetPlayerInfo',
            title:"GetPlayerInfo",
            description:""
        }
    );
    a(
        {
            id:3,
            title:"Piece",
            content:"Piece",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp.Models/Piece',
            title:"Piece",
            description:""
        }
    );
    a(
        {
            id:4,
            title:"PlayerColor",
            content:"PlayerColor",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp/PlayerColor',
            title:"PlayerColor",
            description:""
        }
    );
    a(
        {
            id:5,
            title:"GetDiceThrow",
            content:"GetDiceThrow",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp/GetDiceThrow',
            title:"GetDiceThrow",
            description:""
        }
    );
    a(
        {
            id:6,
            title:"MovePiece",
            content:"MovePiece",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp.Models/MovePiece',
            title:"MovePiece",
            description:""
        }
    );
    a(
        {
            id:7,
            title:"GameModel",
            content:"GameModel",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp.Models/GameModel',
            title:"GameModel",
            description:""
        }
    );
    a(
        {
            id:8,
            title:"GetAddPlayer",
            content:"GetAddPlayer",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp/GetAddPlayer',
            title:"GetAddPlayer",
            description:""
        }
    );
    a(
        {
            id:9,
            title:"PlayerCounter",
            content:"PlayerCounter",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp/PlayerCounter',
            title:"PlayerCounter",
            description:""
        }
    );
    a(
        {
            id:10,
            title:"Program",
            content:"Program",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp/Program',
            title:"Program",
            description:""
        }
    );
    a(
        {
            id:11,
            title:"GameInfo",
            content:"GameInfo",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp.Models/GameInfo',
            title:"GameInfo",
            description:""
        }
    );
    a(
        {
            id:12,
            title:"GetCurrentPlayer",
            content:"GetCurrentPlayer",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp/GetCurrentPlayer',
            title:"GetCurrentPlayer",
            description:""
        }
    );
    a(
        {
            id:13,
            title:"Player",
            content:"Player",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp.Models/Player',
            title:"Player",
            description:""
        }
    );
    a(
        {
            id:14,
            title:"ErrorViewModel",
            content:"ErrorViewModel",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp.Models/ErrorViewModel',
            title:"ErrorViewModel",
            description:""
        }
    );
    a(
        {
            id:15,
            title:"LudoController",
            content:"LudoController",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp.Controllers/LudoController',
            title:"LudoController",
            description:""
        }
    );
    a(
        {
            id:16,
            title:"GetWinner",
            content:"GetWinner",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp.ApiDataRequest/GetWinner',
            title:"GetWinner",
            description:""
        }
    );
    a(
        {
            id:17,
            title:"GameList",
            content:"GameList",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp.Models/GameList',
            title:"GameList",
            description:""
        }
    );
    a(
        {
            id:18,
            title:"GetGameInfo",
            content:"GetGameInfo",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp/GetGameInfo',
            title:"GetGameInfo",
            description:""
        }
    );
    a(
        {
            id:19,
            title:"ModifyPlayerStartPosition",
            content:"ModifyPlayerStartPosition",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp/ModifyPlayerStartPosition',
            title:"ModifyPlayerStartPosition",
            description:""
        }
    );
    a(
        {
            id:20,
            title:"IPlayerCounter",
            content:"IPlayerCounter",
            description:'',
            tags:''
        },
        {
            url:'/api/Hackerman_WebbApp/IPlayerCounter',
            title:"IPlayerCounter",
            description:""
        }
    );
    var idx = lunr(function() {
        this.field('title');
        this.field('content');
        this.field('description');
        this.field('tags');
        this.ref('id');
        this.use(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
        documents.forEach(function (doc) { this.add(doc) }, this)
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
