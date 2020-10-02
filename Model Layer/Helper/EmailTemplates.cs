using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model_Layer.Helper
{
    
        public static class EmailTemplates
        {
            public static string FPTemplate = "<!DOCTYPE html" +
                    "<html lang='en'>" +
                    "<!--[if lte IE 6]><html class='preIE7 preIE8 preIE9'><![endif]-->" +
                    "<!--[if IE 7]><html class='preIE8 preIE9'><![endif]-->" +
                    "<!--[if IE 8]><html class='preIE9'><![endif]-->" +
                    "<head>" +
                    " <meta charset = 'UTF-8' >" +
                    "<meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1'>" +
                    "<link href='https://fonts.googleapis.com/css?family=Poppins&display=swap' rel='stylesheet'>" +
                    "<style>" +
                    "*{margin:0 !important;padding:0 !important; box-sizing:border-box !important;}" +
                    ".container {width: 100% !important;height: 100% !important;}" +
                    ".outer-div { position: absolute !important;left: 50% !important;top: 50% !important; transform: translate(-50%, -50%) !important; background-color: #fff !important;}" +
                    "img {width: 60px;height: 60px;margin: 7px 0;}" +
                    ".inner-div {background: rgb(251, 251, 251) !important;width: 650px !important; max-height: 500px;border-top: 4px solid dodgerblue; padding: 38px 56px;font-family: 'Poppins',sans-serif !important; border-radius: 4px;border-bottom: 4px solid rgb(127, 170, 212);}" +
                    " h2 {font-size: 30px;}" +
                    "p {font-size: 15px !important;margin: 10px 0;line-height: 21px;font-weight: 500;}" +
                    ".lead {color: #848484 !important; font-variant: all-petite-caps !important; padding-top: 30px !important;}" +
                    ".button-box {text-align:center !important; margin: 36px 0 !important;}" +
                    ".link-btn {text-decoration: none !important; color: #fff !important;background: dodgerblue;outline: none; border: none;padding: 10px 40px; border-radius: 5px;cursor: pointer;}" +
                    " .link {font-style: italic;color: dodgerblue;cursor: pointer;text-decoration: none;  font-size:13px !important}" +
                    " .img-box { text-align:center !important; width: 712px !important;display: flex !important;justify-content: center !important; -ms-justify-content: center !important;align-items: center !important;}" +
                    ".tt{font-size: 28px !important; font-family: 'Poppins', sans-serif !important;font-weight: bold; margin-left: 5px;color: dodgerblue;}" +
                    "</style>" +
                    "</head>" +
                    "<body>" +
                    "<div class='container'>" +
                    "<div class='outer-div'>" +
                    "<div class='img-box'>" +
                    "<img src='https://images.squarespace-cdn.com/content/5db43ffa6420426c488984f4/1572162997606-PM0BVWNHHQXV91CP11VA/unicorn+%281%29.png?content-type=image%2Fpng' alt='buildmyunicorn'>" +
                    "<span class='tt'>Build my Unicorn</span>" +
                    "</div>" +
                    "<div class='inner-div'>" +
                    "<h2>Confirm Your Email</h2>" +
                    "<p>Hi <span style='text-transform: capitalize !important; font-weight:600 !important;'> @NAME</span>,</p>" +
                    "<p>Thank you for signing-up with Build my Unicorn, we are here to help you start your startup. Tap the button below to confirm your email.</p>" +
                    "<div class='button-box'>" +
                     "<a href='@URL' class='link-btn btn-primary' style='font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; color: #fff; text-decoration: none; line-height: 2em; font-weight: bold; text-align: center; cursor: pointer; display: inline-block; border-radius: 5px; text-transform: capitalize; background-color: #5fbeaa; margin: 0; border-color: #5fbeaa; border-style: solid; border-width: 10px 20px;'>Confirm Email</a>" +
                    "</div>" +
                    "<p class='lead'>if that doesn't work, copy and paste the following link in your browser:</p>" +
                    "<p class='link'>@URL</p>" +
                    "</div>" +
                    "</div>" +
                    "</div>" +
                    "</body>" +
                    "</html>";


            public static Dictionary<string, string> Template =
            new Dictionary<string, string>()
                {
                                  {"FP", FPTemplate.ToString()},
                 };

        }
    }
