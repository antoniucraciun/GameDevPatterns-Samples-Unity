using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;        // PointerUpLinkTagEvent (clickable <link> tags)

namespace GameDevPatterns.Tools
{
    // Window > GameDevPatterns > README Viewer
    // Shows the README of whatever sample folder (or .md file) is selected in the Project window,
    // rendered as formatted text. Links work: web links open the browser, relative links open
    // the linked README here and ping it in the Project window.
    // Supports the Markdown the sample READMEs use: headings, paragraphs, lists, quotes, tables,
    // code blocks, **bold**, *italic*, `code` and [links](url).
    public class ReadmeViewer : EditorWindow
    {
        [SerializeField] string currentPath;              // relative to the project root
        [SerializeField] bool followSelection = true;
        [SerializeField] List<string> history = new List<string>();

        ScrollView content;
        Label pathLabel;

        static string ProjectRoot => Path.GetDirectoryName(Application.dataPath);
        static string Full(string path) => Path.Combine(ProjectRoot, path);
        static string LinkColor => EditorGUIUtility.isProSkin ? "#58A6FF" : "#0B5CAD";

        [MenuItem("Window/GameDevPatterns/README Viewer")]
        public static void Open() => GetWindow<ReadmeViewer>("README");

        void OnEnable() => Selection.selectionChanged += OnSelectionChanged;
        void OnDisable() => Selection.selectionChanged -= OnSelectionChanged;
        void OnFocus() => Render();                       // pick up edits made in a text editor

        void CreateGUI()
        {
            var toolbar = new Toolbar();
            toolbar.Add(new ToolbarButton(Back) { text = "◀ Back", tooltip = "Previous README" });
            toolbar.Add(new ToolbarButton(() => Show("README.md")) { text = "Repo README" });
            toolbar.Add(new ToolbarButton(OpenInEditor) { text = "Edit", tooltip = "Open this file in your text editor" });
            var follow = new ToolbarToggle { text = "Follow selection", value = followSelection };
            follow.RegisterValueChangedCallback(e => followSelection = e.newValue);
            toolbar.Add(follow);
            rootVisualElement.Add(toolbar);

            pathLabel = new Label();
            pathLabel.style.opacity = 0.6f;
            pathLabel.style.paddingLeft = 8;
            pathLabel.style.paddingTop = 4;
            rootVisualElement.Add(pathLabel);

            content = new ScrollView();
            content.style.flexGrow = 1;
            content.contentContainer.style.paddingLeft = 12;
            content.contentContainer.style.paddingRight = 12;
            content.contentContainer.style.paddingBottom = 16;
            rootVisualElement.Add(content);

            if (string.IsNullOrEmpty(currentPath)) OnSelectionChanged();
            Render();
        }

        // ── Navigation ─────────────────────────────────────────────────────

        void OnSelectionChanged()
        {
            if (!followSelection) return;
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (string.IsNullOrEmpty(path)) return;

            if (AssetDatabase.IsValidFolder(path))
            {
                string readme = path + "/README.md";
                if (File.Exists(Full(readme))) Show(readme);
            }
            else if (path.EndsWith(".md"))
            {
                Show(path);
            }
        }

        void Show(string path)
        {
            if (path == currentPath) return;
            if (!string.IsNullOrEmpty(currentPath)) history.Add(currentPath);
            currentPath = path;
            Render();
        }

        void Back()
        {
            if (history.Count == 0) return;
            currentPath = history[history.Count - 1];
            history.RemoveAt(history.Count - 1);
            Render();
        }

        void OpenInEditor()
        {
            if (!string.IsNullOrEmpty(currentPath) && File.Exists(Full(currentPath)))
                EditorUtility.OpenWithDefaultApp(Full(currentPath));
        }

        void FollowLink(string url)
        {
            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                Application.OpenURL(url);
                return;
            }

            string target = Resolve(Path.GetDirectoryName(currentPath), url.Split('#')[0]);
            if (Directory.Exists(Full(target))) target += "/README.md";
            if (!File.Exists(Full(target)))
            {
                Debug.LogWarning($"README Viewer: link target not found: {target}");
                return;
            }

            Show(target);
            var asset = AssetDatabase.LoadAssetAtPath<Object>(target);
            if (asset != null) EditorGUIUtility.PingObject(asset);
        }

        // Joins a relative link onto a folder and collapses "." and "..".
        static string Resolve(string folder, string relative)
        {
            var parts = new List<string>();
            foreach (string part in ((folder ?? "") + "/" + relative).Replace('\\', '/').Split('/'))
            {
                if (part == "" || part == ".") continue;
                if (part == ".." && parts.Count > 0) parts.RemoveAt(parts.Count - 1);
                else parts.Add(part);
            }
            return string.Join("/", parts);
        }

        // ── Rendering ──────────────────────────────────────────────────────

        static readonly Regex Heading = new Regex(@"^(#{1,6})\s+(.*)$");
        static readonly Regex ListItem = new Regex(@"^(\s*)([-*]|\d+\.)\s+(.*)$");
        static readonly Regex TableSeparator = new Regex(@"^\|?\s*:?-{3,}");

        void Render()
        {
            if (content == null) return;
            content.Clear();

            if (string.IsNullOrEmpty(currentPath) || !File.Exists(Full(currentPath)))
            {
                pathLabel.text = "";
                content.Add(Text("Select a sample folder or a README.md in the Project window."));
                return;
            }

            pathLabel.text = currentPath;
            string[] lines = File.ReadAllText(Full(currentPath)).Replace("\r\n", "\n").Split('\n');

            int i = 0;
            while (i < lines.Length)
            {
                string line = lines[i];
                string trimmed = line.Trim();

                if (trimmed.Length == 0) { i++; continue; }

                if (trimmed.StartsWith("```"))
                {
                    var code = new StringBuilder();
                    for (i++; i < lines.Length && !lines[i].Trim().StartsWith("```"); i++)
                        code.AppendLine(lines[i]);
                    i++;
                    content.Add(CodeBlock(code.ToString().TrimEnd()));
                    continue;
                }

                var heading = Heading.Match(line);
                if (heading.Success)
                {
                    content.Add(HeadingLabel(heading.Groups[2].Value, heading.Groups[1].Length));
                    i++;
                    continue;
                }

                if (trimmed.StartsWith("|"))
                {
                    var rows = new List<string>();
                    while (i < lines.Length && lines[i].Trim().StartsWith("|")) rows.Add(lines[i++].Trim());
                    content.Add(Table(rows));
                    continue;
                }

                if (trimmed.StartsWith(">"))
                {
                    var quote = new StringBuilder();
                    while (i < lines.Length && lines[i].Trim().StartsWith(">"))
                        quote.Append(lines[i++].Trim().TrimStart('>').Trim()).Append(' ');
                    content.Add(Quote(quote.ToString().Trim()));
                    continue;
                }

                var item = ListItem.Match(line);
                if (item.Success)
                {
                    // The item, plus indented continuation lines that don't start another block.
                    var text = new StringBuilder(item.Groups[3].Value);
                    for (i++; i < lines.Length && lines[i].StartsWith(" ") && lines[i].Trim().Length > 0
                                && !IsBlockStart(lines[i]); i++)
                        text.Append(' ').Append(lines[i].Trim());
                    content.Add(ListRow(item.Groups[2].Value, text.ToString(), item.Groups[1].Value.Length / 2));
                    continue;
                }

                var paragraph = new StringBuilder();
                while (i < lines.Length && lines[i].Trim().Length > 0 && (paragraph.Length == 0 || !IsBlockStart(lines[i])))
                    paragraph.Append(lines[i++].Trim()).Append(' ');
                content.Add(Text(paragraph.ToString().Trim()));
            }
        }

        static bool IsBlockStart(string line)
        {
            string t = line.Trim();
            return t.StartsWith("```") || t.StartsWith("|") || t.StartsWith(">")
                || Heading.IsMatch(line) || ListItem.IsMatch(line);
        }

        Label Text(string markdown, bool bold = false)
        {
            var label = new Label(Inline(markdown)) { enableRichText = true };
            label.style.whiteSpace = WhiteSpace.Normal;
            label.style.marginTop = 4;
            label.style.marginBottom = 4;
            if (bold) label.style.unityFontStyleAndWeight = FontStyle.Bold;
            label.RegisterCallback<PointerUpLinkTagEvent>(e => FollowLink(e.linkID));
            return label;
        }

        VisualElement HeadingLabel(string markdown, int level)
        {
            var label = Text(markdown, bold: true);
            label.style.fontSize = level == 1 ? 20 : level == 2 ? 16 : 13;
            label.style.marginTop = level == 1 ? 10 : 14;
            if (level <= 2)
            {
                label.style.paddingBottom = 3;
                label.style.borderBottomWidth = 1;
                label.style.borderBottomColor = new Color(0.5f, 0.5f, 0.5f, 0.35f);
            }
            return label;
        }

        VisualElement Quote(string markdown)
        {
            var box = new VisualElement();
            box.style.borderLeftWidth = 3;
            box.style.borderLeftColor = new Color(0.5f, 0.5f, 0.5f, 0.6f);
            box.style.paddingLeft = 10;
            box.style.marginTop = 4;
            box.style.marginBottom = 4;
            var label = Text(markdown);
            label.style.opacity = 0.85f;
            box.Add(label);
            return box;
        }

        VisualElement ListRow(string marker, string markdown, int depth)
        {
            var row = new VisualElement();
            row.style.flexDirection = FlexDirection.Row;
            row.style.paddingLeft = 6 + depth * 16;
            var bullet = new Label(marker == "-" || marker == "*" ? "•" : marker);
            bullet.style.width = 20;
            bullet.style.marginTop = 2;
            bullet.style.flexShrink = 0;
            var text = Text(markdown);
            text.style.marginTop = 2;
            text.style.marginBottom = 2;
            text.style.flexShrink = 1;
            text.style.flexGrow = 1;
            row.Add(bullet);
            row.Add(text);
            return row;
        }

        static VisualElement CodeBlock(string code)
        {
            var label = new Label { text = code, enableRichText = false };
            label.style.whiteSpace = WhiteSpace.Normal;
            label.style.backgroundColor = new Color(0.5f, 0.5f, 0.5f, 0.15f);
            label.style.paddingLeft = 8;
            label.style.paddingRight = 8;
            label.style.paddingTop = 6;
            label.style.paddingBottom = 6;
            label.style.marginTop = 4;
            label.style.marginBottom = 4;
            label.selection.isSelectable = true;           // so commands can be copied
            return label;
        }

        VisualElement Table(List<string> rows)
        {
            var table = new VisualElement();
            table.style.marginTop = 6;
            table.style.marginBottom = 6;
            var border = new Color(0.5f, 0.5f, 0.5f, 0.3f);
            bool header = true;

            foreach (string row in rows)
            {
                if (TableSeparator.IsMatch(row)) continue;
                var line = new VisualElement();
                line.style.flexDirection = FlexDirection.Row;
                line.style.borderBottomWidth = 1;
                line.style.borderBottomColor = border;
                if (header) line.style.backgroundColor = new Color(0.5f, 0.5f, 0.5f, 0.12f);

                foreach (string cell in row.Trim().Trim('|').Split('|'))
                {
                    var label = Text(cell.Trim(), bold: header);
                    label.style.flexGrow = 1;
                    label.style.flexBasis = 0;
                    label.style.paddingLeft = 6;
                    label.style.paddingRight = 6;
                    line.Add(label);
                }
                table.Add(line);
                header = false;
            }
            return table;
        }

        // Markdown inline syntax → Unity rich text.
        static string Inline(string markdown)
        {
            var result = new StringBuilder();
            foreach (string part in Regex.Split(markdown, "(`[^`]+`)"))
            {
                if (part.Length > 1 && part[0] == '`' && part[part.Length - 1] == '`')
                {
                    result.Append("<mark=#8080803A><noparse>").Append(part, 1, part.Length - 2).Append("</noparse></mark>");
                    continue;
                }

                string text = part.Replace("<", "<noparse><</noparse>");
                text = Regex.Replace(text, @"\[([^\]]+)\]\(([^)\s]+)\)", m => Link(m.Groups[2].Value, m.Groups[1].Value));
                text = Regex.Replace(text, @"(?<![="">\w])https?://[^\s<)]*[^\s<).,;:]", m => Link(m.Value, m.Value));
                text = Regex.Replace(text, @"\*\*(.+?)\*\*", "<b>$1</b>");
                text = Regex.Replace(text, @"(?<![\*\w])\*(?=\S)(.+?)(?<=\S)\*(?![\*\w])", "<i>$1</i>");
                result.Append(text);
            }
            return result.ToString();
        }

        static string Link(string url, string text) =>
            $"<link=\"{url}\"><color={LinkColor}><u>{text}</u></color></link>";
    }
}
