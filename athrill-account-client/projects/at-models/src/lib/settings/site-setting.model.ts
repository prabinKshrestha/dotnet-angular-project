import { BaseModel } from '../at-base.model';
import { FormDataBaseUpdateModel } from '../at-form-data.model';

export class SiteSettingModel extends BaseModel
{
    public SiteSettingId : number;
    public Name : string;
    public Name1 : string;
    public Name2 : string;
    public Name3 : string;
    public Name4 : string;
    public SiteSlogan : string;
    public SiteUrl : string;
    public ImageName : string;
    public FeedbackEmail : string;
    public CopyrightName : string;
    public WorkHours : string;
    
    public AddressShortDetail : string;
    public AddressDetail : string;
    public LocationIframe : string;

    public PhoneNumber : string;
    public FacebookLink : string;
    public YoutubeLink : string;
    public TwiterLink : string;
    public SkypeLink : string;
    public LinkedInLink : string;
    public InstagramLink : string;
    public Viber : string;
    public Whatsapp : string;

    public MetaTitle : string;
    public MetaKeywords : string;
    public MetaDescription : string;
    public MetaImageName : string;

    public Id : number;
    public Image : string;
    public MetaImage : string;
}

export class SiteSettingUpdateModel extends FormDataBaseUpdateModel
{
    public SiteSettingId : number;
    public Name : string;
    public Name1 : string;
    public Name2 : string;
    public Name3 : string;
    public Name4 : string;
    public SiteSlogan : string;
    public SiteUrl : string;
    public FeedbackEmail : string;
    public CopyrightName : string;
    public WorkHours : string;
    public ImageFile : File;
    
    public AddressShortDetail : string;
    public AddressDetail : string;
    public LocationIframe : string;

    public PhoneNumber : string;
    public FacebookLink : string;
    public YoutubeLink : string;
    public TwiterLink : string;
    public SkypeLink : string;
    public LinkedInLink : string;
    public InstagramLink : string;
    public Viber : string;
    public Whatsapp : string;

    public MetaTitle : string;
    public MetaKeywords : string;
    public MetaDescription : string;
    public MetaImageFile : File;

    public IsImageChanged : boolean;
    public IsMetaImageChanged : boolean;
}
