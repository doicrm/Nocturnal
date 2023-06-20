﻿namespace Nocturnal.Core.System;

public static class Constants
{
    // Default game's name and version indentification
    public const string GAME_NAME       = "NOCTURNAL";
    public const string GAME_VERSION    = "[Demo Build]";

    // Game logo
    public static readonly string[] GAME_LOGO = new string[] {
        "\t ****     **   *******     ******  ********** **     ** *******   ****     **     **     **\n",
        "\t/**/**   /**  **/////**   **////**/////**/// /**    /**/**////** /**/**   /**    ****   /**\n",
        "\t/**//**  /** **     //** **    //     /**    /**    /**/**   /** /**//**  /**   **//**  /**\n",
        "\t/** //** /**/**      /**/**           /**    /**    /**/*******  /** //** /**  **  //** /**\n",
        "\t/**  //**/**/**      /**/**           /**    /**    /**/**///**  /**  //**/** **********/**\n",
        "\t/**   //****//**     ** //**    **    /**    /**    /**/**  //** /**   //****/**//////**/**\n",
        "\t/**    //*** //*******   //******     /**    //******* /**   //**/**    //***/**     /**/********\n",
        "\t//      ///   ///////     //////      //      ///////  //     // //      /// //      // ////////\n"
    };

    public const int MAX_SAVES          = 10;

    // Default starting values
    public const int DEFAULT_ATTRIBUTE  = 10;
}
